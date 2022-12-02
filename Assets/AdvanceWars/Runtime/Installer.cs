using System.Collections.Generic;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using AdvanceWars.Runtime.Presentation;
using UnityEngine;
using Zenject;
using Terrain = AdvanceWars.Runtime.Data.Terrain;
using Unit = AdvanceWars.Runtime.Data.Unit;

namespace AdvanceWars.Runtime
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            var player = new Player { Id = "p1" };

            var map = new Map(5, 5);
            map.Put(Vector2Int.zero, Resources.Load<Terrain>("Plain"));
            map.Put(Vector2Int.zero, Resources.Load<Unit>("Infantry").CreateBattalion());
            map.Put(Vector2Int.up, Resources.Load<Terrain>("Forest"));

            var motherland = new Nation("n1");
            var situation = new Situation { Map = map, Treasury = new Treasury(), Motherland = motherland };

            var co = new CommandingOfficer(situation);

            var game = new Game(new[] { co }, new Dictionary<Nation, Player> { { motherland, player } }, map);
            Container.Bind<Game>().FromInstance(game).AsSingle().NonLazy();

            Container.BindInstance(map).AsSingle();
            Container.BindInstance(situation).AsSingle();

            InstallViews();

            Container.Bind<CursorView>().To<Selector>().FromComponentInHierarchy().AsSingle();

            InstallControllers();
        }

        void InstallViews()
        {
            Container.Bind<MapView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SelectionView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MovementView>().To<TweenMovementView>().AsSingle();
            Container.Bind<DayView>().FromComponentInHierarchy().AsSingle();
        }

        void InstallControllers()
        {
            Container.Bind<DrawMap>().AsSingle();
            Container.Bind<CursorController>().AsSingle();
            Container.Bind<SelectBattalion>().AsSingle();
            Container.Bind<MoveBattalion>().AsSingle();
            Container.Bind<EndTurn>().AsSingle();
        }
    }
}