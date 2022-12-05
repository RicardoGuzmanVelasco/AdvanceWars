using System.Collections;
using System.Collections.Generic;
using AdvanceWars.Runtime.Application;
using AdvanceWars.Runtime.Presenters;
using UnityEngine;
using Zenject;

public class ModeSelectionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ConfigGameplay>().AsSingle();
        Container.Bind<PlayersConfigurationView>().FromComponentInHierarchy().AsSingle();
    }
}
