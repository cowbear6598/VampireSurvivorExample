using PlayerController;
using TimeSystem;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMoveHandler : ITickable
    {
        private readonly PlayerView   playerView;
        private readonly ITimeService timeService;
        private readonly IInput       input;

        public PlayerMoveHandler(PlayerView playerView, ITimeService timeService, IInput input)
        {
            this.input       = input;
            this.playerView  = playerView;
            this.timeService = timeService;
        }

        public void Move(float xAxis, float yAxis)
        {
            var transform = playerView.GetTransform();

            var moveDelta = new Vector3(xAxis, yAxis) * timeService.GetDeltaTime();

            transform.Translate(moveDelta);
        }

        public void Tick()
        {
            Move(input.GetHorizontal(), input.GetVertical());
        }
    }
}