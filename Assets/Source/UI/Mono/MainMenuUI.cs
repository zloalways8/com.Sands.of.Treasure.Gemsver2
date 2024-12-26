using System;
using Source.Game;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class MainMenuUI : PoliceConfirmUI
    {
        [Inject] private GameStats _gameStats;

        private void Awake()
        {
            _gameStats.OnStopGame += Show;
            Hide();
        }
    }
}