using System.Collections.Generic;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;
using Zenject;

namespace AdvanceWars.Runtime
{
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            var player = new Player() { Id = "p1" };

            var map = new Map(1, 2);
            var plain = Resources.Load<Runtime.Data.Terrain>("Plain");
            map.Put(Vector2Int.zero, plain);
            var forest = Resources.Load<Runtime.Data.Terrain>("Forest");
            map.Put(Vector2Int.up, forest);

            var motherland = new Nation("n1");
            var situation = new Situation() { Map = map, Treasury = new Treasury(), Motherland = motherland };

            var co = new CommandingOfficer(situation);

            var game = new Game(new[] { co }, new Dictionary<Nation, Player>() { { motherland, player } }, map);
            Container.Bind<Game>().FromInstance(game).AsSingle().NonLazy();

            Container.BindInstance(map).AsSingle();
            Container.Bind<DrawMap>().AsSingle();

            Container.Bind<MapView>().FromComponentInHierarchy().AsSingle();
        }
    }
}