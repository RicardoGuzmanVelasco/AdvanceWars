using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class GameBuilder
    {
        IEnumerable<string> nations = new[] { "SingleNation" };
        Map map = MapBuilder.Map().Of(3, 3).Build();
        bool began;

        #region ObjectMothers
        public static GameBuilder Game() => new();
        #endregion

        public GameBuilder WithNations(params string[] motherlands)
        {
            nations = motherlands;

            return this;
        }

        public GameBuilder On(Map theMap)
        {
            this.map = theMap;
            return this;
        }

        public GameBuilder Of(int playerAmount)
        {
            var nations = new string[playerAmount];

            for(var i = 0; i < playerAmount; i++)
                nations[i] = "Motherland" + i;

            this.nations = nations;

            return this;
        }

        public GameBuilder Began()
        {
            began = true;

            return this;
        }

        public Game Build()
        {
            var players = new Dictionary<Nation, Player>();
            var officers = new List<CommandingOfficer>();

            foreach(var n in nations)
            {
                var allegiance = new Nation(n);
                players.Add(allegiance, new Player { Id = n });
                officers.Add(CommandingOfficerBuilder.CommandingOfficer().Of(allegiance).Build());
            }

            var game = new Game(officers, players, map);

            if(began)
                game.Begin();

            return game;
        }
    }
}