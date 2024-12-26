using System;
using Source.Audio.Mono;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Mono
{
    public class AudioToggle : MonoBehaviour
    {

        [SerializeField] private Image _onImage;
        [SerializeField] private Image _offImage;
        [SerializeField] private string _key;
        [SerializeField] private MusicManager[] _musicManagers;

        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(ValueChange);
            _toggle.isOn = PlayerPrefs.GetInt(_key, 1) == 1;
            ValueChange(_toggle.isOn);
        }

        public void ValueChange(bool value)
        {
            _onImage.enabled = value;
            _offImage.enabled = !value;
            foreach (var musicManager in _musicManagers)
            {
                musicManager.OnOffMusic(value);
            }
            
        }

        private void OnEnable()
        {
            _toggle.isOn = PlayerPrefs.GetInt(_key, 1) == 1;
        }
    }
}