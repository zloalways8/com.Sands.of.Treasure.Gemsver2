using System;
using UnityEngine;

namespace Source.Audio.Mono
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudio;
        [SerializeField] private string _key;
        [SerializeField] private string _muteKey;

        private static int _volume;
        private static float _loudness;
        
        public void OnOffMusic(bool value)
        {
            _volume = value ? 1 : 0;
            PlayerPrefs.SetInt(_muteKey, _volume);
            _musicAudio.mute = !value;
        }

        public void SlideMusic(float val)
        {
            _loudness = val;
            PlayerPrefs.SetFloat(_key, _loudness);
            _musicAudio.volume = _loudness;
        }
    }
}