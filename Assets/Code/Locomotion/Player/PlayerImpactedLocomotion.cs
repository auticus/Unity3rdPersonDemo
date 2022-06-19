using Unity3rdPersonDemo.Locomotion.Player;

namespace Assets.Code.Locomotion.Player
{
    public class PlayerImpactedLocomotion : PlayerLocomotion
    {
        public PlayerImpactedLocomotion(IPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in
        }
    }
}
