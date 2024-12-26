using System;
using Source.Game;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class PausePanelUI : UIView
    {
        [Inject] private GameStats _gameStats;

        private void Awake()
        {
            _gameStats.OnStopGame += Hide;
            Hide();
        }

        public void Resume()
        {
            _gameStats.Resume();
        }

        public void StopGame()
        {
            _gameStats.Menu();
        }
    }
}