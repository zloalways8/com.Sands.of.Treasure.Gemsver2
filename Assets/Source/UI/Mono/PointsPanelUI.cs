using Source.Game;
using TMPro;
using UnityEngine;
using VContainer;

namespace Source.UI.Mono
{
    public class PointsPanelUI : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;

        [SerializeField] private string _scoreName;
        [SerializeField] private TMP_Text _scoreText;
        
        private void Awake()
        {
            _gameStats.OnScoreUpdate += OnScoreUpdate;
        }

        private void OnScoreUpdate(int val, int maxVal)
        {
            _scoreText.text = $"{_scoreName} {val}/{maxVal}";
        }
    }
}