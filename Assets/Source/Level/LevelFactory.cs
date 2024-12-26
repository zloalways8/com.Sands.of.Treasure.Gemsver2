using Source.Level.Mono;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Level
{
    public class LevelFactory
    {
        [Inject] private readonly IObjectResolver _objectResolver;

        private Level _currentLevel;

        public Level Create()
        {
            _currentLevel = _objectResolver.Resolve<Level>();
            Debug.Log("Level create: "+ _currentLevel);
            return _currentLevel;
        }
    }
}