using System;
using Source.Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Source.UI.Mono
{
    public class TimerSliderUI : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;

        [SerializeField] private string _timerName;
        [SerializeField] private Slider _timerSlider;

        [SerializeField] private GameSettingsScriptable _gameSettingsScriptable;
        
        private void Awake()
        {
            _gameStats.OnTimerUpdate += OnTimerUpdate;
            _gameStats.OnStartLevel += OnStartLevel;
        }

        private void OnStartLevel()
        {
            gameObject.SetActive(_gameSettingsScriptable.TimerEnable);
        }

        private void OnTimerUpdate(float val)
        {
            if (_timerSlider)
            {
                _timerSlider.value = val / _gameStats.LevelTime;
            }
            
        }
    }
}