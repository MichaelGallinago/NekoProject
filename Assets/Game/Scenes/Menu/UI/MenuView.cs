using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.SceneReference;

namespace Scenes.Menu.UI
{
    [RequireComponent(
        typeof(MenuViewBinding), typeof(RectTransform), typeof(AudioSource))]
    public class MenuView : MonoBehaviour
    {
        [SerializeField, HideInInspector] private AudioSource _audioSource;
        [SerializeField, HideInInspector] private MenuViewBinding _binding;
        [SerializeField, HideInInspector] private RectTransform _rectTransform;

        [SerializeField] private SceneReference _scene;

        private void OnValidate()
        {
            _audioSource = GetComponent<AudioSource>();
            _binding = GetComponent<MenuViewBinding>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Awake()
        {
            _binding.PlayButton.onClick.AddListener(OnPlayPressed);
            _binding.SettingsButton.onClick.AddListener(OnSettingsPressed);

            StartIntroAsync().Forget();
        }

        private void OnPlayPressed()
        {
            _binding.PlayButton.onClick.RemoveListener(OnPlayPressed);
            _binding.TransitionFade.Play();
            
            _binding.SuikaCat.FlyAwayAsync(_rectTransform.sizeDelta.y).Forget();
            
            TransitFromMenuAsync().Forget();
        }

        private async UniTaskVoid TransitFromMenuAsync()
        {
            await StopMusicAsync();
            SceneManager.LoadScene(_scene);
        }

        private async UniTask StopMusicAsync() => await LSequence.Create()
            .Append(LMotion.Create(_audioSource.pitch, 1.5f, 1f)
                .WithEase(Ease.InBounce)
                .BindToPitch(_audioSource))
            .Append(LMotion.Create(_audioSource.pitch, 0f, 2f)
                .WithEase(Ease.InBounce)
                .BindToPitch(_audioSource))
            .Run()
            .ToUniTask();

        private async UniTaskVoid StartIntroAsync()
        {
            await _binding.NameJapan.ShowTextAsync();
            _audioSource.Play();
        }
        
        private void OnSettingsPressed()
        {
            
        }
    }
}
