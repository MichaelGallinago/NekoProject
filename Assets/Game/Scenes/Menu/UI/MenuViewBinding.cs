using LitMotion.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Menu.UI
{
    public class MenuViewBinding : MonoBehaviour
    {
        [field:SerializeField] public SuikaCat SuikaCat { get; private set; }
        [field:SerializeField] public Button PlayButton { get; private set; }
        [field:SerializeField] public Button SettingsButton { get; private set; }
        [field:SerializeField] public NameJapan NameJapan { get; private set; }
        [field:SerializeField] public LitMotionAnimation TransitionFade { get; private set; }
    }
}
