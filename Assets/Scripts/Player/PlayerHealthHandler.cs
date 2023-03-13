using Character;
using Player;

namespace Tests.Editor
{
    public class PlayerHealthHandler
    {
        private readonly PlayerView    playerView;
        private readonly CharacterData characterData;
        private          int           hp;

        public PlayerHealthHandler(PlayerView playerView, CharacterData characterData)
        {
            this.playerView    = playerView;
            this.characterData = characterData;

            hp = characterData.hp;
        }

        public int GetCurrentHealth() => hp;
    }
}