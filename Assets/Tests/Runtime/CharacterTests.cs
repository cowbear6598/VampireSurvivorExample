using System.Collections;
using System.Threading.Tasks;
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

            await GivenACharacter();

            Assert.IsNotNull(Object.FindObjectOfType<CharacterView>());
        });

        private async Task<GameObject> GivenACharacter()
        {
            characterFactory = Container.Resolve<CharacterFactory>();

            var character = await characterFactory.Create(0);
            return character;
        }

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