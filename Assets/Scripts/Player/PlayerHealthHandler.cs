using Character;
using Player;

namespace Tests.Editor
{
    public class PlayerHealthHandler
    {
        private readonly CharacterData characterData;
        private readonly PlayerView    playerView;
        private          int           hp;

        public PlayerHealthHandler(PlayerView playerView, CharacterData characterData)
        {
            this.playerView    = playerView;
            this.characterData = characterData;

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