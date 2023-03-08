using System.Collections;
using Character;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using TimeSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.Runtime
{
    public class CharacterTests : ZenjectIntegrationTestFixture
    {
        private CharacterFactory characterFactory;
        private ITimeService     timeService;

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

                Assert.IsNotNull(Container.Resolve<CharacterView>());
            });
        }

        [UnityTest]
        [TestCase(-1, 0, -1, 0, ExpectedResult = null)]
        [TestCase(0, 1, 0, 1, ExpectedResult = null)]
        [TestCase(1, 0, 1, 0, ExpectedResult = null)]
        [TestCase(0, -1, 0, -1, ExpectedResult = null)]
        [TestCase(1, 1, 1, 1, ExpectedResult = null)]
        [TestCase(1, -1, 1, -1, ExpectedResult = null)]
        [TestCase(-1, -1, -1, -1, ExpectedResult = null)]
        [TestCase(-1, 1, -1, 1, ExpectedResult = null)]
        public IEnumerator Should_Character_Move(float expectedX, float expectedY, float xAxis, float yAxis)
        {
            return UniTask.ToCoroutine(async () =>
            {
                Install();

                await GivenACharacter();

                var characterMoveHandler = Container.Resolve<CharacterMoveHandler>();
                var characterView        = Container.Resolve<CharacterView>();

                characterMoveHandler.Move(xAxis, yAxis);

                Assert.AreEqual(expectedX, characterView.transform.position.x);
                Assert.AreEqual(expectedY, characterView.transform.position.y);
            });
        }

        private async UniTask GivenACharacter()
        {
            await characterFactory.Create(0);

            Container.Bind<CharacterView>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<CharacterMoveHandler>().AsSingle();
        }

        private void Install()
        {
            PreInstall();

            BindCharacter();

            Container.BindInterfacesAndSelfTo<FakeTimeService>().AsSingle();

            PostInstall();
        }

        private void BindCharacter()
        {
            var characterSettings = AssetDatabase.LoadAssetAtPath<CharacterSettingsInstaller>("Assets/Data/CharacterSettings.asset");

            Container.BindInstance(characterSettings.factorySetting).IfNotBound();
            Container.Bind<CharacterFactory>().AsSingle();

            characterFactory = Container.Resolve<CharacterFactory>();
        }
    }
}