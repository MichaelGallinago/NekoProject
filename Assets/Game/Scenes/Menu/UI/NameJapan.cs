using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;

namespace Scenes.Menu.UI
{
    [RequireComponent(typeof(TextMeshProUGUI), typeof(AudioSource))]
    public class NameJapan : MonoBehaviour
    {
        [SerializeField] private float _duration = 1.2f;
        
        [SerializeField, HideInInspector] private AudioSource _audioSource;
        [SerializeField, HideInInspector] private TextMeshProUGUI _textMeshPro;

        private string _initialText;
        private float _soundDelay;

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        private void Awake() => SetUpValues();
        
        public async UniTask ShowTextAsync()
        {
            PlayAppearSoundsAsync().Forget();
            await PlayAppearAnimation();
        }

        private void SetUpValues()
        {
            _initialText = _textMeshPro.text;
            _soundDelay = _duration / _initialText.Length;
        }

        private async UniTask PlayAppearAnimation() => 
            await LMotion.String.Create32Bytes(string.Empty, _initialText, _duration)
                .BindToText(_textMeshPro);

        private async UniTaskVoid PlayAppearSoundsAsync()
        {
            for (var i = 0; i < _initialText.Length; i++)
                await PlayAppearSoundAsync();
        }

        private async UniTask PlayAppearSoundAsync()
        {
            await UniTask.WaitForSeconds(_soundDelay);
            _audioSource.Play();
        }
    }
}
