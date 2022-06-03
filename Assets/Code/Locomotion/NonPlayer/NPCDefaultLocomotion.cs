using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    /// <summary>
    /// NPC Locomotion with no player targets.
    /// </summary>
    public class NPCDefaultLocomotion : NonPlayerLocomotion
    {
        private readonly int SpeedHash = Animator.StringToHash("Speed"); //pass a hash instead of a string as the int is faster
        public NPCDefaultLocomotion(INonPlayerMoveable character) : base(character)
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
            //todo: currently he just sits there and does nothing
            return Vector3.zero;
        }
    }
}