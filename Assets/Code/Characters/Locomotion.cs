using Unity3rdPersonDemo.Combat.Targeting;
using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    /// <summary>
    /// Class responsible for moving characters.
    /// </summary>
    public class Locomotion
    {
        private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed"); //pass a hash instead of a string as the int is faster
        private readonly IMoveableState _character;
        private const float DAMP_SMOOTH_TIME = 0.1f;
        private const int IDLE = 0;
        private const int RUNNING = 1;

        public Locomotion(IMoveableState character)
        {
            _character = character;
        }

        /// <summary>
        /// Processes Locomotion that is driven by external input / controls. 
        /// </summary>
        /// <param name="character">The character to whom the movement applies.</param>
        /// <param name="deltaTime">The delta time tick that applies to the movement.</param>
        public void ProcessInputDrivenLocomotion(float deltaTime)
        {
            var movement = CalculateRelativeMovementVector();
            if (movement == Vector3.zero)
            {
                _character.Animator.SetFloat(_freeLookSpeedHash, IDLE, DAMP_SMOOTH_TIME, deltaTime);
                return;
            }

            _character.Animator.SetFloat(_freeLookSpeedHash, RUNNING, DAMP_SMOOTH_TIME, deltaTime);
            HandleMovement(movement, deltaTime);
            HandleRotation(movement, deltaTime);
        }

        /// <summary>
        /// Given a target, will turn to face that target.
        /// </summary>
        /// <param name="target">The target to face.</param>
        public void FaceTarget(Target target)
        {
            if (target == null) return;
            var facingVector = target.transform.position - _character.EntityTransform.position;
            facingVector.y = 0; //we don't care about the height difference
            _character.EntityTransform.rotation = Quaternion.LookRotation(facingVector);
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
        {
            _character.CharacterController.Move((moveVector * _character.FreeLookMovementSpeed + _character.Force.MovementForce) * deltaTime);
        }

        private void HandleRotation(Vector3 moveVector, float deltaTime) =>
            _character.EntityTransform.rotation = Quaternion.Lerp(
                _character.EntityTransform.rotation,
                Quaternion.LookRotation(moveVector),
                deltaTime * _character.FreeLookRotationDamping);
    }
}
