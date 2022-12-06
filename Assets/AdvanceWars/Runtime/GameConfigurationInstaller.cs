using System.Collections;
using System.Collections.Generic;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Data;
using AdvanceWars.Runtime.Presenters;
using UnityEngine;
using Zenject;

public class GameConfigurationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ConfigGameplay>().AsSingle();
        Container.Bind<PlayersConfigurationView>().FromComponentInHierarchy().AsSingle();

        Container.BindInstance(new GameBuilder()).AsSingle();
    }
}
