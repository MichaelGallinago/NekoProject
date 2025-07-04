using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using UnityEngine;

namespace Scenes.Menu.UI
{
    [RequireComponent(
        typeof(RectTransform), typeof(AudioSource), typeof(LitMotionAnimation))]
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private float _hidingDelay;
        
        [SerializeField, HideInInspector] private AudioSource _audioSource;
        [SerializeField, HideInInspector] private LitMotionAnimation _animation;

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            _animation = GetComponent<LitMotionAnimation>();
        }

        private void Awake()
        {
            Show();
            Hide().Forget();
        }

        private void Show()
        {
            Vector3 scale = transform.localScale;
            scale.y = 1f;
            transform.localScale = scale;
        }
        
        private async UniTaskVoid Hide()
        {
            await UniTask.WaitForSeconds(_hidingDelay);
            
            _audioSource.Play();
            _animation.Play();
        }
    }
}
