using System.Threading.Tasks;
using AdvanceWars.Runtime.Data;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Troops;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Terrain = AdvanceWars.Runtime.Data.Terrain;
using Unit = AdvanceWars.Runtime.Data.Unit;

namespace AdvanceWars.Runtime.Application
{
    public class ConfigGameplay
    {
        readonly ZenjectSceneLoader sceneLoader;
        readonly GameBuilder gameBuilder;

        PlayersConfigurationView playersConfigurationView;

        public ConfigGameplay(ZenjectSceneLoader sceneLoader, PlayersConfigurationView playersConfigurationView)
        {
            this.gameBuilder = new GameBuilder();
            this.sceneLoader = sceneLoader;
            this.playersConfigurationView = playersConfigurationView;
        }

        public Task AddPlayer()
        {
            gameBuilder.AddPlayer();
            return playersConfigurationView.SetPlayers(gameBuilder.Players);
        }

        public void Run()
        {
            var map = new Map(5, 5);
            map.Put(Vector2Int.zero, Resources.Load<Terrain>("Plain"));
            map.Put(Vector2Int.zero, Resources.Load<Unit>("Infantry").CreateBattalion(new Nation("n1")));
            map.Put(Vector2Int.up, Resources.Load<Terrain>("Forest"));

            var game = gameBuilder.WithMap(map).Build();
            sceneLoader.LoadSceneAsync("WalkingSkeleton", LoadSceneMode.Single,
                (container) => { container.BindInstance(game); });
        }

        public Task RemovePlayer()
        {
            gameBuilder.RemovePlayer();
            return playersConfigurationView.SetPlayers(gameBuilder.Players);
        }
    }
}