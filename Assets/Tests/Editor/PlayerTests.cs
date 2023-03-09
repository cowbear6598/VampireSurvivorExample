using Character;
using NSubstitute;
using NUnit.Framework;
using Player;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void _01_Spawn_Player()
        {
            var playerFactory    = Substitute.For<IPlayerFactory>();
            var characterSpawner = new CharacterSpawner(playerFactory);

            characterSpawner.SpawnPlayer(0);

            playerFactory.Received(1).Create(0);
        }

        [Test]
        public void _02_Player_Move()
        {
            var characterObject = new GameObject("Player");
            var playerView      = characterObject.AddComponent<PlayerView>();

            var playerMoveHandler = new PlayerMoveHandler(playerView);

            playerMoveHandler.Move(1, 0);

            Assert.AreEqual(1, playerView.GetTransform().position.x);
        }
    }
}