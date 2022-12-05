using AdvanceWars.Runtime.Data;
using AdvanceWars.Runtime.Domain.Map;
using AdvanceWars.Runtime.Domain.Orders;
using AdvanceWars.Runtime.Presentation;
using UnityEngine.SceneManagement;
using Zenject;

namespace AdvanceWars.Runtime.Application
{
    public class LoadGameplay
    {
        readonly ZenjectSceneLoader sceneLoader;
        public LoadGameplay(ZenjectSceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }

        public void Run()
        {
            var game = GameBuilder.Game().OfPlayers(1).Build();
            sceneLoader.LoadSceneAsync("WalkingSkeleton", LoadSceneMode.Single, (container) =>
            {
                container.BindInstance(game).WhenInjectedInto<GameplayInstaller>();
            });
        }
    }
}