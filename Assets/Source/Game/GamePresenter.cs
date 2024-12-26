using VContainer;
using VContainer.Unity;

namespace Source.Game
{
    public class GamePresenter: IStartable, ITickable
    {
        [Inject] private readonly GameStats _gameStats;
        
        public void Start()
        {
            
        }

        public void Tick()
        {
            _gameStats.UpdateTimer();
        }
    }
}