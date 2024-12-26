using System;
using Source.Game;
using Source.Level.Mono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Mono
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _startLevelButton;
        [SerializeField] private TMP_Text _levelNameText;
        [SerializeField] private string _levelPrefix;
        private GameStats _gameStats;

        private int _index = 0;

        private void Awake()
        {
            _startLevelButton = GetComponent<Button>();
        }

        public void Init(GameStats gameStats, int index)
        {
            _gameStats = gameStats;
            _index = index;
            _levelNameText.text = $"{_levelPrefix}{_index+1}";
            _gameStats.OnWinLevel += CheckOpen;
            CheckOpen();
            _startLevelButton.onClick.AddListener(StartLevel);
        }

        private void CheckOpen()
        {
            if (_index != 0)
            {
                _startLevelButton.interactable = PlayerPrefs.GetInt("Level_"+_index, 0) == 1;
            }
        }

        private void StartLevel()
        {
            _gameStats.StartLevel(_index);
        }
    }
}