using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerFreeLookLocomotion : ILocomotion
    {
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed"); //pass a hash instead of a string as the int is faster
        private readonly IMoveableState _character;
        
        public PlayerFreeLookLocomotion(IMoveableState character)
        {
            _character = character;
        }

        public void Process(float deltaTime)
        {
            var movement = CalculateRelativeMovementVector();
            if (movement == Vector3.zero)
            {
                _character.Animator.SetFloat(FreeLookSpeedHash, Locomotion.IDLE, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
                return;
            }

            _character.Animator.SetFloat(FreeLookSpeedHash, Locomotion.RUNNING, Locomotion.DAMP_SMOOTH_TIME, deltaTime);
            HandleMovement(movement, deltaTime);
            HandleRotation(movement, deltaTime);
        }

        private Vector3 CalculateRelativeMovementVector()
        {
            //calculate the movement vector based on the input of the gamepad / keyboard plus the forward and right vectors of the camera
            //so that the character moves in the direction that the camera is facing

            var cameraForward = _character.MainCameraTransform.forward;
            var inputForward = _character.InputReader.MovementValue.y; //up/down
            var cameraRight = _character.MainCameraTransform.right;
            var inputRight = _character.InputReader.MovementValue.x; //left/right

            cameraForward.y = 0f; // we don't care about the tilt
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            return (cameraForward * inputForward) + (cameraRight * inputRight);
        }

        private void HandleMovement(Vector3 moveVector, float deltaTime)
            => _character.CharacterController.Move((moveVector * _character.FreeLookMovementSpeed + _character.Force.MovementForce) * deltaTime);
        

        private void HandleRotation(Vector3 moveVector, float deltaTime) =>
            _character.EntityTransform.rotation = Quaternion.Lerp(
                _character.EntityTransform.rotation,
                Quaternion.LookRotation(moveVector),
                deltaTime * _character.FreeLookRotationDamping);
    }
}
