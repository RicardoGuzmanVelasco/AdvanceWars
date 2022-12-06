using System.Threading.Tasks;
using AdvanceWars.Runtime.Data;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Presentation;
using UnityEngine.SceneManagement;
using Zenject;

namespace AdvanceWars.Runtime.Application
{
    public class ConfigGameplay
    {
        readonly ZenjectSceneLoader sceneLoader;
        readonly GameBuilder gameBuilder;
        
        PlayersConfigurationView playersConfigurationView;
        public ConfigGameplay(GameBuilder gameBuilder, ZenjectSceneLoader sceneLoader, PlayersConfigurationView playersConfigurationView)
        {
            this.gameBuilder = gameBuilder;
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
            var game = gameBuilder.Build();
            sceneLoader.LoadSceneAsync("WalkingSkeleton", LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(game).WhenInjectedInto<GameplayInstaller>();
            });
        }

        public Task RemovePlayer()
        {
            gameBuilder.RemovePlayer();
            return playersConfigurationView.SetPlayers(gameBuilder.Players);
        }
    }
}