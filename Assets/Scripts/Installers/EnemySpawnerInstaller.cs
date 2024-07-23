using UnityEngine;
using Zenject;


public class EnemySpawnerInstaller : MonoInstaller
{
    [SerializeField]
    private EnemySpawner _enemySpawner;
    public override void InstallBindings()
    {
        var enemySpawnerInstance = Container.InstantiatePrefabForComponent<EnemySpawner>(_enemySpawner);

        Container.Bind<EnemySpawner>().FromInstance(enemySpawnerInstance).AsSingle().NonLazy();
    }
}
