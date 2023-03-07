using System;
using Character;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameView : MonoBehaviour
    {
        [Inject] private readonly CharacterFactory characterFactory;

        private async void Start()
        {
            await characterFactory.Create(0);
        }
    }
}