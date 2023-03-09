using System;
using Character;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        private CharacterSpawner characterSpawner;
        
        [Inject]
        public void Inject(CharacterSpawner characterSpawner)
        {
            this.characterSpawner = characterSpawner;
        }

        private void Start()
        {
            characterSpawner.SpawnPlayer(0);
        }
    }
}