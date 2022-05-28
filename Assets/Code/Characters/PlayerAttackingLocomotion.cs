using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerAttackingLocomotion : Locomotion
    {
        public PlayerAttackingLocomotion(IMoveableState character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            HandleMovement(deltaTime); //no real movement but HandleMovement also puts gravity and force in
            FaceTarget(Character.ObjectTargeter.CurrentTarget);
        }
    }
}
