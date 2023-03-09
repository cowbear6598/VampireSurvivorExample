using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Player
{
    public interface IPlayerFactory
    {
        UniTask<GameObject> Create(int index);
    }

    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer container;
        private readonly Settings    settings;

        public PlayerFactory(Settings settings, DiContainer container)
        {
            this.settings  = settings;
            this.container = container;
        }

        public async UniTask<GameObject> Create(int index)
        {
            var characterAsset = await Addressables.LoadAssetAsync<GameObject>(settings.playerAssets[index]).Task;

            var characterObj = container.InstantiatePrefab(characterAsset);

            return characterObj;
        }

        [Serializable]
        public class Settings
        {
            public AssetReference[] playerAssets;
        }
    }
}