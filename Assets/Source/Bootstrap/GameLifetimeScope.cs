using Source.CellManagement;
using Source.CellManagement.Mono;
using Source.Game;
using Source.Level;
using Source.Level.Mono;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Source.Bootstrap
{
    public class GameLifetimeScope : LifetimeScope
    {
        [FormerlySerializedAs("_cellViewPrefab")] [SerializeField] private CellMono cellMonoPrefab;
        [FormerlySerializedAs("_itemViewPrefab")] [SerializeField] private ItemMono itemMonoPrefab;
        [FormerlySerializedAs("_levelView")] [SerializeField] private LevelMono levelMono;
        [SerializeField] private GameSettingsScriptable _gameSettingsScriptable;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelPresenter>();
            builder.RegisterEntryPoint<GamePresenter>();
            
            builder.Register<LevelFactory>(Lifetime.Singleton);
            builder.Register<CellsFactory>(Lifetime.Singleton);
            builder.Register<ItemFactory>(Lifetime.Singleton);
            builder.Register<GameStats>(Lifetime.Singleton);
            
            builder.Register<Level.Level>(Lifetime.Scoped);

            builder.RegisterComponent(cellMonoPrefab);
            builder.RegisterComponent(itemMonoPrefab);
            builder.RegisterComponent(levelMono);
            builder.RegisterComponent(_gameSettingsScriptable);
        }
    }
}
