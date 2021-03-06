using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion.Player
{
    public class PlayerFreeLookLocomotion : PlayerLocomotion
    {
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed"); //pass a hash instead of a string as the int is faster
        
        public PlayerFreeLookLocomotion(IPlayerMoveable character) : base(character)
        { }

        public override void Process(float deltaTime)
        {
            var movement = CalculateRelativeMovementVector();
            if (movement == Vector3.zero) //character is standing around and no movement on the controller
            {
                Character.Animator.SetFloat(FreeLookSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
                HandleMovement(deltaTime);
                return;
            }

            Character.Animator.SetFloat(FreeLookSpeedHash, Locomotion.RUN_FORWARD, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            HandleMovement(movement * Character.DefaultMovementSpeed, deltaTime);
            HandleRotation(movement, deltaTime);
        }

        private Vector3 CalculateRelativeMovementVector()
        {
            //calculate the movement vector based on the input of the gamepad / keyboard plus the forward and right vectors of the camera
            //so that the character moves in the direction that the camera is facing

            var cameraForward = Character.MainCameraTransform.forward;
            var inputForward = Character.InputReader.MovementValue.y; //up/down
            var cameraRight = Character.MainCameraTransform.right;
            var inputRight = Character.InputReader.MovementValue.x; //left/right

            cameraForward.y = 0f; // we don't care about the tilt
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            return (cameraForward * inputForward) + (cameraRight * inputRight);
        }
    }
}
