using Source.Game;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class LosePanelUI : UIView
    {
        [Inject] private GameStats _gameStats;
        
        private void Awake()
        {
            _gameStats.OnLoseLevel += OnLoseLevel;
            _gameStats.OnStartLevel += Hide;
            Hide();
        }

        private void OnLoseLevel()
        {
            Show();
        }

        public void Restart()
        {
            _gameStats.Restart();
        }
        
        public void StopGame()
        {
            _gameStats.Menu();
            Hide();
        }
    }
}