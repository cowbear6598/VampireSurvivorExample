using Zenject;

namespace Character
{
    public class CharacterInstaller : MonoInstaller<CharacterInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CharacterMoveHandler>().AsSingle();
        }
    }
}