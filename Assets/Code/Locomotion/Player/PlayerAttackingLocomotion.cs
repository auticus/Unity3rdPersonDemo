namespace Unity3rdPersonDemo.Locomotion.Player
{
    public class PlayerAttackingLocomotion : PlayerLocomotion
    {
        public PlayerAttackingLocomotion(IPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in

            if (Character.ObjectTargeter.CurrentTarget == null) return;
            FaceTarget(Character.ObjectTargeter.CurrentTarget.transform);
        }
    }
}
