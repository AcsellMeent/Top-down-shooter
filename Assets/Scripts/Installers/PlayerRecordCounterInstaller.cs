using UnityEngine;
using Zenject;

public class PlayerRecordCounterInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerRecordCounter _playerRecordCounter;

    [SerializeField]
    private RectTransform _playerRecordCounterOrigin;

    public override void InstallBindings()
    {
        var playerRecordCounterInstance = Container.InstantiatePrefabForComponent<PlayerRecordCounter>(_playerRecordCounter, _playerRecordCounterOrigin);

        Container.Bind<PlayerRecordCounter>().FromInstance(playerRecordCounterInstance).AsSingle().NonLazy();
    }
}
