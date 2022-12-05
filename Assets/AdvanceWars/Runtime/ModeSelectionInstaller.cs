using System.Collections;
using System.Collections.Generic;
using AdvanceWars.Runtime.Application;
using UnityEngine;
using Zenject;

public class ModeSelectionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoadGameplay>().AsSingle();
    }
}
