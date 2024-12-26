using Source.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Source.UI.Mono
{
    public enum TimerType
    {
        MinSec,
        Sec,
    }

    public class TimerPanelUI : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;

        [SerializeField] private string _timerName;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TimerType _timerType;

        private void Awake()
        {
            _gameStats.OnTimerUpdate += OnTimerUpdate;
        }
        
        private void OnTimerUpdate(float val)
        {
            if (_timerText)
            {
                switch (_timerType)
                {
                    case TimerType.MinSec:
                        _timerText.text = $"{_timerName} {Mathf.FloorToInt(val/60f)}m.{Mathf.RoundToInt(val%60f)}s.";
                        break;
                    case TimerType.Sec:
                        _timerText.text = $"{_timerName} {Mathf.RoundToInt(val)}sec.";
                        break;
                }
                
            }

        }
    }
}