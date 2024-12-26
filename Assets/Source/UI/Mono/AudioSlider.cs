using Source.Audio.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Mono
{
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private MusicManager _musicManager;

        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ValueChange);
            _slider.value = PlayerPrefs.GetFloat(_key, 1f);
            ValueChange(_slider.value);
        }

        public void ValueChange(float value)
        {
            _musicManager.SlideMusic(value);
        }

        private void OnEnable()
        {
            _slider.value = PlayerPrefs.GetFloat(_key, 1f);
        }
    }
}