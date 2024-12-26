using VContainer;
using VContainer.Unity;

namespace Source.Level
{
    public class LevelPresenter: IStartable, ITickable
    {
        [Inject] private readonly LevelFactory _levelFactory;

        private Level _currentLevel;
        
        public void Start()
        {
            _currentLevel = _levelFactory.Create();
            //_currentLevel.Generate();
        }

        public void Tick()
        {
            _currentLevel.UpdateCells();
        }
    }
}