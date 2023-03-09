using Character;
using NSubstitute;
using NUnit.Framework;
using Player;
using PlayerController;
using TimeSystem;
using UnityEngine;

namespace Tests.Editor
{
    [TestFixture]
    public class PlayerTests
    {
        [SetUp]
        public void SetUp()
        {
            timeService = Substitute.For<ITimeService>();
            timeService.GetDeltaTime().Returns(1);

            input = Substitute.For<IInput>();
        }

        private ITimeService timeService;
        private IInput       input;

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
            var playerView = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, timeService, input);

            playerMoveHandler.Move(1, 0);

            Assert.AreEqual(1, playerView.GetTransform().position.x);
        }

        [Test]
        public void _03_Player_Move_By_Controller()
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, timeService, input);

            input.GetHorizontal().Returns(1);
            input.GetVertical().Returns(0);

            playerMoveHandler.Tick();

            Assert.AreEqual(1, playerView.GetTransform().position.x);
        }

        private PlayerView GivenAPlayer()
        {
            var characterObject = new GameObject("Player");
            var playerView      = characterObject.AddComponent<PlayerView>();
            return playerView;
        }
    }
}