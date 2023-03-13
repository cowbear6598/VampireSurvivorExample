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

            characterData = new CharacterData(10f, 100);
        }

        private ITimeService  timeService;
        private IInput        input;
        private CharacterData characterData;

        [Test(Description = "生成玩家")]
        public void _01_Spawn_Player()
        {
            var playerFactory    = Substitute.For<IPlayerFactory>();
            var characterSpawner = new CharacterSpawner(playerFactory);

            characterSpawner.SpawnPlayer(0);

            playerFactory.Received(1).Create(0);
        }

        [Test(Description = "生成玩家後初始化移動速度")]
        public void _02_Player_Initialize_MoveSpeed_After_Spawn()
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, characterData, timeService, input);

            Assert.AreEqual(10f, playerMoveHandler.GetMoveSpeed());
        }

        [Test(Description = "玩家移動")]
        [TestCase(10, 0, 1, 0)]
        [TestCase(10, 10, 1, 1)]
        [TestCase(5f, -2f, 0.5f, -0.2f)]
        public void _03_Player_Move(float expectedX, float expectedY, float xAxis, float yAxis)
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, characterData, timeService, input);

            playerMoveHandler.Move(xAxis, yAxis);

            var playerTransform = playerView.GetTransform();
            PlayerPositionShouldBe(expectedX, expectedY, playerTransform);
        }

        [Test(Description = "玩家由控制器移動")]
        [TestCase(10, 0, 1, 0)]
        [TestCase(-10, -10, -1, -1)]
        [TestCase(-5f, 2f, -0.5f, 0.2f)]
        public void _04_Player_Move_By_Controller(float expectedX, float expectedY, float xAxis, float yAxis)
        {
            var playerView        = GivenAPlayer();
            var playerMoveHandler = new PlayerMoveHandler(playerView, characterData, timeService, input);

            input.GetHorizontal().Returns(xAxis);
            input.GetVertical().Returns(yAxis);

            playerMoveHandler.Tick();

            var playerTransform = playerView.GetTransform();
            PlayerPositionShouldBe(expectedX, expectedY, playerTransform);
        }

        [Test(Description = "玩家生成後初始化血量")]
        public void _05_Player_Initialize_HP_Data_After_Spawn()
        {
            var playerView          = GivenAPlayer();
            var playerHealthHandler = new PlayerHealthHandler(playerView, characterData);

            PlayerHpShouldBe(100, playerHealthHandler);
        }

        [Test(Description = "玩家受到攻擊後血量應該也要減少到指定")]
        [TestCase(99, 1)]
        [TestCase(0, 500)]
        [TestCase(100, -100)]
        public void _06_Player_Should_Decrease_Hp_After_Taken_Damage(int expectedHp, int damage)
        {
            var playerView          = GivenAPlayer();
            var playerHealthHandler = new PlayerHealthHandler(playerView, characterData);

            playerHealthHandler.TakenDamage(damage);

            PlayerHpShouldBe(expectedHp, playerHealthHandler);
        }

        private void PlayerHpShouldBe(int expectedHp, PlayerHealthHandler playerHealthHandler)
        {
            Assert.AreEqual(expectedHp, playerHealthHandler.GetCurrentHealth());
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