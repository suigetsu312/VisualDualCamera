using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameObjectTools >().AsSingle();
        Container.Bind<DualCamera >().AsSingle();

    }

}
