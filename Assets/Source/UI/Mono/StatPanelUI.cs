using Source.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Source.UI.Mono
{
    public class StatPanelUI : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;
        
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _levelName;
        [SerializeField] private Slider _timerSlider;

        private void Awake()
        {
            _gameStats.OnTimerUpdate += OnTimerUpdate;
            _gameStats.OnLevelUpdate += OnLevelUpdate;
        }

        private void OnLevelUpdate(int val)
        {
            if (_levelName)
            {
                _levelName.text = $"Level: {val}";
            }
        }

        private void OnTimerUpdate(float val)
        {
            if (_timerText)
            {
                _timerText.text = $"Timer: {Mathf.FloorToInt(val/60f)}m.{Mathf.RoundToInt(val%60f)}s.";
            }

            if (_timerSlider)
            {
                _timerSlider.value = val / _gameStats.LevelTime;
            }
            
        }
    }
}