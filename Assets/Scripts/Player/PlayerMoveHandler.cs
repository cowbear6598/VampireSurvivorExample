using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMoveHandler : ITickable
    {
        private readonly PlayerView playerView;

        public PlayerMoveHandler(PlayerView playerView)
        {
            this.playerView = playerView;
        }

        public void Move(float xAxis, float yAxis)
        {
            var transform = playerView.GetTransform();

            var moveDelta = new Vector3(xAxis, yAxis);

            transform.Translate(moveDelta);
        }

        public void Tick()
        {
            Move(1, 0);
        }
    }
}