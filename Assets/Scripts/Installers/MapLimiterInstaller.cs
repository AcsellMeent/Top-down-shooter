using UnityEngine;
using Zenject;

public class MapLimiterInstaller : MonoInstaller
{
    [SerializeField]
    private MapLimiter _mapLimiter;

    public override void InstallBindings()
    {
        Container.Bind<MapLimiter>().FromComponentInHierarchy(_mapLimiter).AsSingle();
    }
}
