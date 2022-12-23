using AdvanceWars.Runtime.Application;
using Zenject;

public class GameConfigurationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ConfigGameplay>().AsSingle();
        Container.Bind<PlayersConfigurationView>().FromComponentInHierarchy().AsSingle();
    }
}