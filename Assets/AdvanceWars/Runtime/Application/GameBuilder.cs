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
        public int Players { get; private set; } = 1;
        public Map Map { get; private set; } = Map.Null;
        
        public static GameBuilder Game() => new GameBuilder();

        public GameBuilder OfPlayers(int amount)
        {
            Players = amount;
            return this;
        }

        public GameBuilder AddPlayer()
        {
            Players++;
            return this;
        }

        public GameBuilder RemovePlayer()
        {
            if (Players > 1)
                Players--;
            return this;
        }

        public GameBuilder WithMap(Map map)
        {
            this.Map = map;
            return this;
        }

        public Game Build()
        {
            var playersDictionary = new Dictionary<Nation, Player>();
            var commandingOfficers = new CommandingOfficer[Players];
            for (var i = 0; i < Players; i++)
            {
                var nation = new Nation("n" + (i + 1));
                commandingOfficers[i] = CreateCO(Map, nation);
                playersDictionary.Add(nation, new Player {Id = "p" + (i + 1)});
            }

            return new Game(commandingOfficers, playersDictionary, Map);
        }
        
        CommandingOfficer CreateCO(Map map, Nation motherland)
        {
            var situation = new Situation {Map = map, Treasury = new Treasury(), Motherland = motherland};

            var co = new CommandingOfficer(situation);
            return co;
        }
    }
}