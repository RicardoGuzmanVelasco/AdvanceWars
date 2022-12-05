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

        int players = 1;
        PlayersConfigurationView playersConfigurationView;
        public ConfigGameplay(ZenjectSceneLoader sceneLoader, PlayersConfigurationView playersConfigurationView)
        {
            this.sceneLoader = sceneLoader;
            this.playersConfigurationView = playersConfigurationView;
        }

        
        public void AddPlayer()
        {
            players++;
            playersConfigurationView.SetPlayers(players);
        }
        
        public void Run()
        {
            var game = GameBuilder.Game().OfPlayers(players).Build();
            sceneLoader.LoadSceneAsync("WalkingSkeleton", LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(game).WhenInjectedInto<GameplayInstaller>();
            });
        }
    }
}