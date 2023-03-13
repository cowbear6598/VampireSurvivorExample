using System;

namespace Character
{
    [Serializable]
    public class CharacterData
    {
        public float moveSpeed;
        public int   hp;

        public CharacterData(float moveSpeed, int hp)
        {
            this.moveSpeed = moveSpeed;
            this.hp        = hp;
        }
    }
}