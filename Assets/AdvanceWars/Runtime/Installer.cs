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

            var map = new Map(5, 5);
            map.Put(Vector2Int.zero, Resources.Load<Terrain>("Plain"));
            map.Put(Vector2Int.zero, Resources.Load<Unit>("Infantry").CreateBattalion(new Nation("n1")));
            map.Put(Vector2Int.up, Resources.Load<Terrain>("Forest"));

            var co1 = CreateCO(map, new Nation("n1"));
            var co2 = CreateCO(map, new Nation("n2"));
            var player1 = new Player { Id = "p1" };
            var player2 = new Player { Id = "p2" };

            var game = new Game(
                new[]
                {
                    co1, 
                    co2
                }, new Dictionary<Nation, Player>
                {
                    { new Nation("n1"), player1 }, 
                    { new Nation("n2"), player2 }
                }, map);
            Container.Bind<Game>().FromInstance(game).AsSingle().NonLazy();

            Container.BindInstance(map).AsSingle();

            InstallViews();

            Container.Bind<CursorView>().To<Selector>().FromComponentInHierarchy().AsSingle();

            InstallControllers();
        }

        static CommandingOfficer CreateCO(Map map, Nation motherland)
        {
            var situation = new Situation {Map = map, Treasury = new Treasury(), Motherland = motherland};

            var co = new CommandingOfficer(situation);
            return co;
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
            Container.Bind<SelectBattalion>().AsSingle();
            Container.Bind<MoveBattalion>().AsSingle();
            Container.Bind<EndTurn>().AsSingle();
            Container.Bind<WaitBattalion>().AsSingle();
        }
    }
}