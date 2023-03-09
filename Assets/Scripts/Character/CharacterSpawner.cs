using Player;

namespace Character
{
    public class CharacterSpawner
    {
        private readonly IPlayerFactory playerFactory;

        public CharacterSpawner(IPlayerFactory playerFactory)
        {
            this.playerFactory = playerFactory;
        }

        public void SpawnPlayer(int index)
        {
            playerFactory.Create(index);
        }
    }
}