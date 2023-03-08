using SoapUtils.SceneSystem;
using TimeSystem;
using Zenject;

namespace Main
{
    public class MainInstaller : MonoInstaller<MainInstaller>
    {
        public override void InstallBindings()
        {
            BindSceneService();

            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
        }

        private void BindSceneService()
        {
            Container.Bind<LoadHandler>().AsSingle();
            Container.Bind<StateHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
        }
    }
}