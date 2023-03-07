using SoapUtils.SceneSystem;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LoadHandler>().AsSingle();
            Container.Bind<StateHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
        }
    }
}