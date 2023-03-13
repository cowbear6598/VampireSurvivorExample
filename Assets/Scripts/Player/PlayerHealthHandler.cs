using Character;

namespace Player
{
    public class PlayerHealthHandler
    {
        private int hp;

        public PlayerHealthHandler(CharacterData characterData)
        {
            hp = characterData.hp;
        }

        public int GetCurrentHealth()
        {
            return hp;
        }

        public void TakenDamage(int damage)
        {
            if (damage > 0)
                hp -= damage;

            if (hp < 0)
                hp = 0;
        }
    }
}