using Source.Game;
using TMPro;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class LevelPanelUI : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;

        [SerializeField] private string _levelText;
        [SerializeField] private TMP_Text _levelName;
        
        private void Awake()
        {
            _gameStats.OnLevelUpdate += OnLevelUpdate;
        }

        private void OnLevelUpdate(int val)
        {
            if (_levelName)
            {
                _levelName.text = $"{_levelText} {val}";
            }
        }
    }
}