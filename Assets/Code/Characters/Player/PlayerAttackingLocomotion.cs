using UnityEngine;

namespace Unity3rdPersonDemo.Characters.Player
{
    public class PlayerAttackingLocomotion : PlayerLocomotion
    {
        public PlayerAttackingLocomotion(IPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in
            FaceTarget(Character.ObjectTargeter.CurrentTarget);
        }
    }
}
