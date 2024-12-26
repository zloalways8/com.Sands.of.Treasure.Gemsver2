using Source.Game;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Source.UI.Mono
{
    public class PointsSlider : MonoBehaviour
    {
        [Inject] private GameStats _gameStats;

        [SerializeField] private string _pointsName;
        [SerializeField] private Slider _pointsSlider;
        
        private void Awake()
        {
            _gameStats.OnScoreUpdate += OnScoreUpdate;
        }
        
        private void OnScoreUpdate(int val, int maxVal)
        {
            if (_pointsSlider)
            {
                _pointsSlider.value = (float)val / maxVal;
            }
            
        }
    }
}