using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerTargetingLocomotion : ILocomotion
    {
        private readonly IMoveableState _character;

        public PlayerTargetingLocomotion(IMoveableState character)
        {
            _character = character;
        }

        public void Process(float deltaTime)
        {
            // move relative to the target.  When we go right or left we orbit the target.
            // the player should always be facing the target.

            var movementVector = CalculateRelativeMovementVector();
            HandleMovement(movementVector, deltaTime);
        }

        private Vector3 CalculateRelativeMovementVector()
        {
            var movement = new Vector3();
            movement += _character.EntityTransform.right * _character.InputReader.MovementValue.x;
            movement += _character.EntityTransform.forward * _character.InputReader.MovementValue.y;
            return movement;
        }

        private void HandleMovement(Vector3 moveVector, float deltaTime)
            => _character.CharacterController.Move(motion: _character.TargetingMovementSpeed  * deltaTime * moveVector);
    }
}
