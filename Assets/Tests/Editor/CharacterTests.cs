using Character;
using NUnit.Framework;
using UnityEditor;
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
        }

        [Test]
        public void Should_Load_Character_Settings()
        {
            Assert.AreEqual(1, characterSettings.factorySetting.characterAssets.Length);
        }
    }
}