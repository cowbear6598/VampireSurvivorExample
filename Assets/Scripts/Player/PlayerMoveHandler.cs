using Character;
using PlayerController;
using TimeSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMoveHandler : ITickable
    {
        private readonly PlayerView    playerView;
        private readonly CharacterData characterData;
        private readonly ITimeService  timeService;
        private readonly IInput        input;

        public PlayerMoveHandler(PlayerView playerView, CharacterData characterData, ITimeService timeService, IInput input)
        {
            this.input         = input;
            this.playerView    = playerView;
            this.characterData = characterData;
            this.timeService   = timeService;
        }

        public void Tick()
        {
            var xAxis = input.GetHorizontal();
            var yAxis = input.GetVertical();
            
            Move(xAxis, yAxis);
        }

        public void Move(float xAxis, float yAxis)
        {
            var transform = playerView.GetTransform();

            var moveDelta = new Vector3(xAxis, yAxis) * timeService.GetDeltaTime() * characterData.moveSpeed;

            transform.Translate(moveDelta);
        }

        public float GetMoveSpeed() => characterData.moveSpeed;
    }
}