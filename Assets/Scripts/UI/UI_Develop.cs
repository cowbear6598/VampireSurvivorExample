using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UI_Develop: MonoBehaviour
    {
        private DiContainer container;

        [Inject]
        public void Inject(DiContainer container)
        {
            this.container = container;
        }
        
        public void Btn_PlayerDecreaseHp()
        {
            PlayerHealthHandler healthHandler = container.Resolve<PlayerHealthHandler>();
            
            if (healthHandler == null)
            {
                Debug.Log("player health module cannot find");
                return;
            }

            healthHandler.TakenDamage(5);
        }
    }
}