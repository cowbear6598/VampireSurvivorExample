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
        public IEnumerator Should_Spawn_Character_Success()
        {
            return UniTask.ToCoroutine(async () =>
            {
                Install();

                await GivenACharacter();

                Assert.IsNotNull(Object.FindObjectOfType<CharacterView>());
            });
        }

        [UnityTest]
        [TestCase(1, 0, 1, 0, ExpectedResult = null)]
        [TestCase(0, 1, 0, 1, ExpectedResult = null)]
        public IEnumerator Should_Character_Move(float expectedX, float expectedY, float xAxis, float yAxis)
        {
            return UniTask.ToCoroutine(async () =>
            {
                Install();

                await GivenACharacter();

                Container.Bind<CharacterView>().FromNewComponentOnNewGameObject().AsSingle();
                Container.Bind<CharacterMoveHandler>().AsSingle();

                var characterMoveHandler = Container.Resolve<CharacterMoveHandler>();

                characterMoveHandler.Move(xAxis, yAxis);

                var characterView = Container.Resolve<CharacterView>();

                Assert.AreEqual(expectedX, characterView.transform.position.x);
                Assert.AreEqual(expectedY, characterView.transform.position.y);
            });
        }

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