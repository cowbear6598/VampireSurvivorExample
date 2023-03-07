using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Character
{
    public class CharacterFactory
    {
        private readonly Setting setting;

        public CharacterFactory(Setting setting)
        {
            this.setting = setting;
        }
        
        public async void Create(int index)
        {
            
        }
        
        [Serializable]
        public class Setting
        {
            public AssetReference[] characterAssets;
        }
    }
}