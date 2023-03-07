using System.Collections;
using Character;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace Tests.Runtime
{
    public class CharacterTests : ZenjectIntegrationTestFixture
    {
        [UnityTest]
        public IEnumerator Should_Load_Character_Settings_Success()
        {
            PreInstall();
            
            var characterSettings = AssetDatabase.LoadAssetAtPath<CharacterSettingsInstaller>("Assets/Data/CharacterSettings.asset");

            Container.BindInstance(characterSettings.factorySetting).IfNotBound();

            PostInstall();
            
            Assert.AreEqual(1, Container.Resolve<CharacterFactory.Setting>().characterAssets.Length);

            yield return new WaitForEndOfFrame();
        }
    }
}