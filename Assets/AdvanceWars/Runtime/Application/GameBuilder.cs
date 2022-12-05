using System.Collections.Generic;
using AdvanceWars.Runtime.Domain;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;

namespace AdvanceWars.Runtime.Data
{
    public class GameBuilder
    {
        int players;
        Map map = Map.Null;
        
        public static GameBuilder Game() => new GameBuilder();

        public GameBuilder OfPlayers(int amount)
        {
            players = amount;
            return this;
        }

        public GameBuilder WithMap(Map map)
        {
            this.map = map;
            return this;
        }

        public Game Build()
        {
            var playersDictionary = new Dictionary<Nation, Player>();
            var commandingOfficers = new CommandingOfficer[players];
            for (var i = 0; i < players; i++)
            {
                var nation = new Nation("n" + (i + 1));
                commandingOfficers[i] = CreateCO(map, nation);
                playersDictionary.Add(nation, new Player {Id = "p" + (i + 1)});
            }

            return new Game(commandingOfficers, playersDictionary, map);
        }
        
        static CommandingOfficer CreateCO(Map map, Nation motherland)
        {
            var situation = new Situation {Map = map, Treasury = new Treasury(), Motherland = motherland};

            var co = new CommandingOfficer(situation);
            return co;
        }
    }
}