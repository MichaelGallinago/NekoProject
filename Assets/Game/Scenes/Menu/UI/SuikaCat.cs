using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Animation;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Scenes.Menu.UI
{
    [RequireComponent(typeof(Button), typeof(RectTransform), typeof(AudioSource))]
    public class SuikaCat : MonoBehaviour
    {
        [SerializeField] private float _flightDuration;
        [SerializeField] private LitMotionAnimation _idleAnimation;
        
        [SerializeField, HideInInspector] private Button _button;
        [SerializeField, HideInInspector] private AudioSource _audioSource;
        [SerializeField, HideInInspector] private RectTransform _rectTransform;
        
        private void OnValidate()
        {
            _button = GetComponent<Button>();
            _audioSource = GetComponent<AudioSource>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Awake() => _button.onClick.AddListener(PlayClickSound);

        public async UniTaskVoid FlyAwayAsync(float distance)
        {
            float positionY = _rectTransform.position.y;
            float angleZ = _rectTransform.eulerAngles.z;
            float targetAngle = angleZ >= 180f ? 0f : 360f;
            
            _idleAnimation.Stop();

            await LSequence.Create()
                .Join(LMotion.Create(positionY, positionY + distance, _flightDuration)
                    .WithEase(Ease.InOutBack)
                    .BindToPositionY(_rectTransform))
                .Join(LMotion.Create(angleZ, targetAngle, _flightDuration)
                    .BindToEulerAnglesZ(_rectTransform))
                .Run();
        }

        private void PlayClickSound()
        {
            _audioSource.pitch = 0.75f + Random.value * 0.5f;
            _audioSource.Play();
        }
    }
}
