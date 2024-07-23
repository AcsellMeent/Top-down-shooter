using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Transform _playerSpawnPoint;

    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefab(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);

        Container.Bind<PlayerWeaponHandler>().FromInstance(playerInstance.GetComponent<PlayerWeaponHandler>()).AsSingle().NonLazy();
        Container.Bind<PlayerHealth>().FromInstance(playerInstance.GetComponent<PlayerHealth>()).AsSingle().NonLazy();
    }
}
