using System;
using Character;
using Player;
using UnityEngine;
using Zenject;

namespace Game
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public PlayerSettings playerSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings.factorySettings).IfNotBound();
            Container.BindInstance(playerSettings.CharacterData).IfNotBound();
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerFactory.Settings factorySettings;
        public CharacterData          CharacterData;
    }
}