using TimeSystem;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterMoveHandler
    {
        private readonly CharacterView view;
        private readonly ITimeService  time;

        public CharacterMoveHandler(CharacterView view, ITimeService time)
        {
            this.view = view;
            this.time = time;
        }

        public void Move(float xAxis, float yAxis)
        {
            Transform transform = view.transform;

            var moveDelta = new Vector3(xAxis, yAxis) * time.GetDeltaTime();
            transform.Translate(moveDelta);
        }
    }
}