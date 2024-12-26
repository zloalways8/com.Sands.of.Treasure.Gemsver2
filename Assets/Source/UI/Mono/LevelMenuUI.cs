using System;
using Source.Game;
using Source.Level.Mono;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using VContainer;

namespace Source.UI.Mono
{
    public class LevelMenuUI : UIView
    {
        [SerializeField] private Transform _levelPanel;
        [SerializeField] private LevelButton _levelButtonPrefab;

        [Inject] private GameSettingsScriptable _gameSettingsScriptable;
        [Inject] private GameStats _gameStats;

        private void Awake()
        {
            _gameStats.OnStartLevel += Hide;
            int index = 0;
            foreach (var levelScriptable in _gameSettingsScriptable.LevelScriptables)
            {
                LevelButton newLevelButton = Instantiate(_levelButtonPrefab, _levelPanel);
                newLevelButton.Init(_gameStats, index);
                index++;
            }
        }
    }
}