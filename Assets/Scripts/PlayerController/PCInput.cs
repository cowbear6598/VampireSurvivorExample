namespace PlayerController
{
    public class PCInput : IInput
    {
        private PlayerInput playerInput;

        public PCInput()
        {
            playerInput = new PlayerInput();
            playerInput.Enable();
        }
        
        public float GetHorizontal()
        {
            return playerInput.Player.Horizontal.ReadValue<float>();
        }

        public float GetVertical()
        {
            return playerInput.Player.Vertical.ReadValue<float>();
        }
    }
}