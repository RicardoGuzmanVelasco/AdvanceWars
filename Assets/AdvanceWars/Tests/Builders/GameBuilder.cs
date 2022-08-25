using System.Collections.Generic;
using AdvanceWars.Runtime;

namespace AdvanceWars.Tests.Builders
{
    internal class GameBuilder
    {
        IEnumerable<string> nations;
        bool began;

        #region ObjectMothers
        public static GameBuilder Game() => new GameBuilder();
        #endregion

        public GameBuilder WithNations(params string[] motherlands)
        {
            nations = motherlands;

            return this;
        }

        public GameBuilder WithNations(IEnumerable<string> motherlands)
        {
            nations = motherlands;

            return this;
        }

        public GameBuilder Of(int playerAmount)
        {
            var nations = new string[playerAmount];

            for(int i = 0; i < playerAmount; i++)
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

            var game = new Game(officers, players);

            if(began)
                game.Begin();

            return game;
        }
    }
}