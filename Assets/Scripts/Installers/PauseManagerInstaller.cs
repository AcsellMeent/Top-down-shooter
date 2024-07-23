using Zenject;

// Я бы мог поместить PauseManager в Project context, но не сделал этого так как не хочу заниматься очисткой списка интерфейсов при переключении сцен 
public class PauseManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PauseManager>().AsSingle().NonLazy();
    }
}
