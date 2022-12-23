using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Data;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Runtime.Presentation;
using UnityEngine;
using Zenject;
using Terrain = AdvanceWars.Runtime.Data.Terrain;
using Unit = AdvanceWars.Runtime.Data.Unit;

namespace AdvanceWars.Runtime
{
    public class GameplayInstaller : MonoInstaller
    {
        [InjectOptional] Game game;

        public override void InstallBindings()
        {
            var map = new Map(5, 5);
            map.Put(Vector2Int.zero, Resources.Load<Terrain>("Plain"));
            map.Put(Vector2Int.zero, Resources.Load<Unit>("Infantry").CreateBattalion(new Nation("n1")));
            map.Put(Vector2Int.up, Resources.Load<Terrain>("Forest"));

            game ??= GameBuilder.Game().OfPlayers(2).WithMap(map).Build();

            Container.Bind<Game>().FromInstance(game).AsCached().NonLazy();

            Container.BindInstance(game.Battleground).AsSingle();

            InstallViews();

            Container.Bind<CursorView>().To<Selector>().FromComponentInHierarchy().AsSingle();

            InstallControllers();
            InstallInputs();
        }

        void InstallViews()
        {
            Container.Bind<MapView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SelectionView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MovementView>().To<TweenMovementView>().AsSingle();
            Container.Bind<DayView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<TurnView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<WaitView>().To<ColorWaitView>().AsSingle();
        }

        void InstallControllers()
        {
            Container.Bind<DrawMap>().AsSingle();
            Container.Bind<CursorController>().AsSingle();
            Container.Bind<SelectSpace>().AsSingle();
            Container.Bind<MoveBattalion>().AsSingle();
            Container.Bind<EndTurn>().AsSingle();
            Container.Bind<WaitBattalion>().AsSingle();
            Container.Bind<OrderBattalion>().AsSingle();
            Container.Bind<Gameplay>().AsSingle();
            Container.Bind<PlayTurn>().AsSingle();
        }

        void InstallInputs()
        {
            Container.Bind<EndTurnInput>().FromComponentInHierarchy().AsSingle();
        }
    }
}