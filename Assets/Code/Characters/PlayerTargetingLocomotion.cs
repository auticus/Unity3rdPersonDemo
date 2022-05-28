using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerTargetingLocomotion : Locomotion
    {
        private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed"); //pass a hash instead of a string as the int is faster
        private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed"); //pass a hash instead of a string as the int is faster

        public PlayerTargetingLocomotion(IMoveableState character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            // move relative to the target.  When we go right or left we orbit the target.
            // the player should always be facing the target.
            var forward = Character.InputReader.MovementValue.y;
            var right = Character.InputReader.MovementValue.x;

            var movementVector = CalculateRelativeMovementVector(forward, right);
            SetAnimationStateBasedOnInput(right, forward, deltaTime);
            HandleMovement(Character.TargetingMovementSpeed * movementVector, deltaTime);
            FaceTarget(Character.ObjectTargeter.CurrentTarget);
        }

        private Vector3 CalculateRelativeMovementVector(float forward, float right)
        {
            var movement = new Vector3();
            movement += Character.EntityTransform.right * right;
            movement += Character.EntityTransform.forward * forward;
            return movement;
        }

        private void SetAnimationStateBasedOnInput(float inputX, float inputY, float deltaTime)
        {
            if (TrySetIdleAnimations(inputX, inputY, deltaTime)) return;
            SetForwardMovingAnimation(inputY, deltaTime);
            SetStrafeMovingAnimation(inputX, deltaTime);
        }

        private bool TrySetIdleAnimations(float inputX, float inputY, float deltaTime)
        {
            if (inputY != 0 || inputX != 0) return false;
            Character.Animator.SetFloat(TargetingForwardSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            Character.Animator.SetFloat(TargetingRightSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            return true;
        }

        private void SetForwardMovingAnimation(float inputY, float deltaTime)
        {
            if (inputY != 0)
            {
                var forward = inputY > 0 ? Locomotion.RUN_FORWARD : Locomotion.RUN_BACKWARD;
                Character.Animator.SetFloat(TargetingForwardSpeedHash, forward, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            }
            else
            {
                Character.Animator.SetFloat(TargetingForwardSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            }
        }

        private void SetStrafeMovingAnimation(float inputX, float deltaTime)
        {
            if (inputX != 0)
            {
                var right = inputX > 0 ? Locomotion.STRAFE_RIGHT : Locomotion.STRAFE_LEFT;
                Character.Animator.SetFloat(TargetingRightSpeedHash, right, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            }
            else
            {
                Character.Animator.SetFloat(TargetingRightSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            }
        }
    }
}
