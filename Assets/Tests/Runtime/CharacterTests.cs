using System.Collections;
using Character;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.Runtime
{
    public class CharacterTests : ZenjectIntegrationTestFixture
    {
        private CharacterFactory characterFactory;

        [UnityTest]
        public IEnumerator Should_Load_Character_Settings_Success()
        {
            Install();

            Assert.AreEqual(1, Container.Resolve<CharacterFactory.Setting>().characterAssets.Length);

            yield return new WaitForEndOfFrame();
        }

        [UnityTest]
        public IEnumerator Should_Spawn_Character_Success() => UniTask.ToCoroutine(async () =>
        {
            Install();

            characterFactory = Container.Resolve<CharacterFactory>();

            await characterFactory.Create(0);

            Assert.IsNotNull(Object.FindObjectOfType<CharacterView>());
        });

        private void Install()
        {
            PreInstall();

            var characterSettings = AssetDatabase.LoadAssetAtPath<CharacterSettingsInstaller>("Assets/Data/CharacterSettings.asset");

            Container.BindInstance(characterSettings.factorySetting).IfNotBound();

            Container.Bind<CharacterFactory>().AsSingle();
            
            PostInstall();
        }
    }
}