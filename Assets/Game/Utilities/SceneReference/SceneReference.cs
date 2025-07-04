using UnityEngine;

namespace Utilities.SceneReference
{
    [System.Serializable]
    public struct SceneReference
    {
        [SerializeField] private string _path;

        public static implicit operator string(SceneReference reference) => reference._path;
    }
}