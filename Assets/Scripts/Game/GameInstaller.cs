using Character;
using Player;
using Zenject;

namespace Game
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle();

            Container.Bind<CharacterSpawner>().AsSingle();
        }
    }
}