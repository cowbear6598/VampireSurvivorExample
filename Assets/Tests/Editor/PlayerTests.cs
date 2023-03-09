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

        [Test(Description = "生成玩家")]
        public void _01_Spawn_Player()
        {
            var playerFactory    = Substitute.For<IPlayerFactory>();
            var characterSpawner = new CharacterSpawner(playerFactory);

            characterSpawner.SpawnPlayer(0);

            playerFactory.Received(1).Create(0);
        }

        [Test(Description = "玩家移動")]
        [TestCase(1, 0, 1, 0)]
        [TestCase(1, 1, 1, 1)]
        [TestCase(0.5f, -0.2f, 0.5f, -0.2f)]
        public void _02_Player_Move(float expectedX, float expectedY, float xAxis, float yAxis)
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, timeService, input);

            playerMoveHandler.Move(xAxis, yAxis);

            var playerTransform = playerView.GetTransform();
            PlayerPositionShouldBe(expectedX, expectedY, playerTransform);
        }

        [Test(Description = "玩家由控制器移動")]
        [TestCase(1, 0, 1, 0)]
        [TestCase(-1, -1, -1, -1)]
        [TestCase(-0.5f, 0.2f, -0.5f, 0.2f)]
        public void _03_Player_Move_By_Controller(float xAxis, float yAxis, float expectedX, float expectedY)
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, timeService, input);

            input.GetHorizontal().Returns(xAxis);
            input.GetVertical().Returns(yAxis);

            playerMoveHandler.Tick();

            var playerTransform = playerView.GetTransform();
            PlayerPositionShouldBe(expectedX, expectedY, playerTransform);
        }

        private void PlayerPositionShouldBe(float expectedX, float expectedY, Transform playerTransform)
        {
            Assert.AreEqual(expectedX, playerTransform.position.x);
            Assert.AreEqual(expectedY, playerTransform.position.y);
        }

        private PlayerView GivenAPlayer()
        {
            var characterObject = new GameObject("Player");
            var playerView      = characterObject.AddComponent<PlayerView>();
            return playerView;
        }
    }
}