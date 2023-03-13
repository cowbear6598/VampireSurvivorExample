using System;

namespace Character
{
    [Serializable]
    public class CharacterData
    {
        public float moveSpeed;

        public CharacterData(float moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }
    }
}