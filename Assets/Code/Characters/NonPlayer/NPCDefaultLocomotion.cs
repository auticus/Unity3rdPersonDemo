using UnityEngine;

namespace Unity3rdPersonDemo.Characters.NonPlayer
{
    public class NPCDefaultLocomotion : Locomotion
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed"); //pass a hash instead of a string as the int is faster
        public NPCDefaultLocomotion(IMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            var movement = CalculateRelativeMovementVector();
            if (movement == Vector3.zero)
            {
                Character.Animator.SetFloat(SpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
                return;
            }

            Character.Animator.SetFloat(SpeedHash, Locomotion.RUN_FORWARD, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            HandleMovement(movement * Character.DefaultMovementSpeed, deltaTime);
            HandleRotation(movement, deltaTime);
        }

        private Vector3 CalculateRelativeMovementVector()
        {
            //todo:
            return Vector3.zero;
        }
    }
}