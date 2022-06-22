namespace Unity3rdPersonDemo.Locomotion.Player
{
    public class PlayerNoUserInputLocomotion : PlayerLocomotion
    {
        public PlayerNoUserInputLocomotion(IPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in
        }
    }
}
