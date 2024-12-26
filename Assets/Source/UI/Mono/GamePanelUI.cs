using System;
using Source.Game;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using VContainer;

namespace Source.UI.Mono
{
    public class GamePanelUI : UIView
    {
        [Inject] private GameStats _gameStats;

        private void Awake()
        {
            _gameStats.OnStartLevel += Show;
            _gameStats.OnStopGame += Hide;
            _gameStats.OnLoseLevel += Hide;
            _gameStats.OnWinLevel += Hide;
            Hide();
        }

        public void RestartLevel()
        {
            _gameStats.Restart();
        }

        public void PauseLevel()
        {
            _gameStats.Pause();
        }

        public void BackMenu()
        {
            _gameStats.Menu();
        }
    }
}