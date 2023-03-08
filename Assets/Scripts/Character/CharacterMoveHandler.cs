using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterMoveHandler
    {
        private readonly CharacterView view;

        public CharacterMoveHandler(CharacterView view)
        {
            this.view = view;
        }

        public void Move(float xAxis, float yAxis)
        {
            Transform transform = view.transform;

            transform.Translate(new Vector3(xAxis, yAxis));
        }
    }
}