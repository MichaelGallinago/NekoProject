using UnityEditor;
using UnityEngine;

namespace Utilities.SceneReference.Editor
{
    [CustomPropertyDrawer(typeof(SceneReference))]
    public class SceneReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty pathProp = property.FindPropertyRelative("_path");
            string path = pathProp.stringValue;

            SceneAsset sceneAsset = !string.IsNullOrEmpty(path)
                ? AssetDatabase.LoadAssetAtPath<SceneAsset>(path)
                : null;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            var newSceneAsset = EditorGUI.ObjectField(
                position, label, sceneAsset, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                string newPath = newSceneAsset != null ? AssetDatabase.GetAssetPath(newSceneAsset) : "";
                pathProp.stringValue = newPath;
            }

            EditorGUI.EndProperty();
        }
    }
}