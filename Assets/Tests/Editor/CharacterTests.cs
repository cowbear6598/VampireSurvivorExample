using Character;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Tests.Editor
{
    public class CharacterTests : ZenjectUnitTestFixture
    {
        private CharacterSettingsInstaller characterSettings;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            characterSettings = AssetDatabase.LoadAssetAtPath<CharacterSettingsInstaller>("Assets/Data/CharacterSettings.asset");

            Container.BindInstance(characterSettings.factorySetting).IfNotBound();
            Container.Bind<CharacterFactory>().AsSingle();
        }

        [Test]
        public void Load_Character_Settings()
        {
            Assert.AreEqual(1, characterSettings.factorySetting.characterAssets.Length);
        }

        [Test]
        public void Spawn_Character()
        {
            var characterFactory = Container.Resolve<CharacterFactory>();

        }
    }
}