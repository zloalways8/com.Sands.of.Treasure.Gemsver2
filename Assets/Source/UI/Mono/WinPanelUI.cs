using System;
using Source.Game;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class WinPanelUI : UIView
    {
        [Inject] private GameStats _gameStats;

        private void Awake()
        {
            _gameStats.OnWinLevel += OnWinLevel;
            _gameStats.OnStartLevel += Hide;
            Hide();
        }

        private void OnWinLevel()
        {
            Show();
        }

        public void NextLevel()
        {
            _gameStats.NextLevel();
        }

        public void StopGame()
        {
            _gameStats.Menu();
            Hide();
        }
    }
}