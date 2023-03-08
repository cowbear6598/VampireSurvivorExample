using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Character
{
    public class CharacterFactory
    {
        private readonly Setting     setting;
        private readonly DiContainer container;

        public CharacterFactory(Setting setting, DiContainer container)
        {
            this.setting   = setting;
            this.container = container;
        }

        public async UniTask<GameObject> Create(int index)
        {
            var loadAsync = Addressables.LoadAssetAsync<GameObject>(setting.characterAssets[index]);

            var characterObj = container.InstantiatePrefab(await loadAsync.Task);

            Addressables.Release(loadAsync);
            
            return characterObj;
        }

        [Serializable]
        public class Setting
        {
            public AssetReference[] characterAssets;
        }
    }
}