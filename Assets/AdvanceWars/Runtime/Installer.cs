using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;
using Zenject;
using Terrain = AdvanceWars.Runtime.Domain.Map.Terrain;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        var player = new Player() { Id = "p1" };

        var map = new Map(1, 1);
        map.Put(Vector2Int.zero, Terrain.Null);

        var motherland = new Nation("n1");
        var situation = new Situation() { Map = map, Treasury = new Treasury(), Motherland = motherland };

        var co = new CommandingOfficer(situation);

        var game = new Game(new[] { co }, new Dictionary<Nation, Player>() { { motherland, player } }, map);
        Container.Bind<Game>().FromInstance(game).AsSingle().NonLazy();
    }
}