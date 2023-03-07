using UnityEngine;
using Zenject;

namespace Character
{
    [CreateAssetMenu(fileName = "Character Settings", menuName = "Data/Character Settings")]
    public class CharacterSettingsInstaller : ScriptableObjectInstaller<CharacterSettingsInstaller>
    {
        public CharacterFactory.Setting factorySetting;
        
        public override void InstallBindings()
        {
            Container.BindInstance(factorySetting);
        }
    }
}