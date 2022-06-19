using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion
{
    /// <summary>
    /// Class responsible for moving characters.
    /// </summary>
    public abstract class Locomotion : ILocomotion
    {
        public const float DAMP_SMOOTH_TIME = 0.1f;
        public const int IDLE = 0;
        public const int RUN_FORWARD = 1;
        public const int RUN_BACKWARD = -1;
        public const int STRAFE_RIGHT = 1;
        public const int STRAFE_LEFT = -1;
        protected readonly IMoveable Character;

        protected Locomotion(IMoveable character)
        {
            Character = character;
        }

        /// <summary>
        /// Processes Locomotion based on the type of state passed to it. 
        /// </summary>
        /// <param name="deltaTime">The delta time tick that applies to the movement.</param>
        public abstract void Process(float deltaTime);

        /// <summary>
        /// Handles movement involving an idle character or one that has no player-inputted movement direction applied.
        /// </summary>
        /// <param name="deltaTime"></param>
        protected void HandleMovement(float deltaTime) 
            => HandleMovement(Vector3.zero, deltaTime);

        /// <summary>
        /// Handles moving the character and applying gravity and force against the character.
        /// </summary>
        /// <param name="moveVector">The movement vector to move along.</param>
        /// <param name="deltaTime"></param>
        protected void HandleMovement(Vector3 moveVector, float deltaTime)
            => Character.CharacterController.Move((moveVector + Character.Force.MovementForce) * deltaTime);

        protected void HandleRotation(Vector3 moveVector, float deltaTime) =>

            Character.EntityTransform.rotation = Quaternion.Lerp(
                Character.EntityTransform.rotation,
                Quaternion.LookRotation(moveVector),
                deltaTime * Character.RotationDamping);

        /// <summary>
        /// Given a target, will give the Vector to that target with a Y vector of 0.
        /// </summary>
        /// <param name="target">The target to get a vector to.</param>
        /// <returns></returns>
        protected virtual Vector3 GetVectorXZToTarget(Transform target)
        {
            if (target == null) return Vector3.zero;
            var facingVector = target.position - Character.EntityTransform.position;
            facingVector.y = 0; //we don't care about the height difference

            return facingVector;
        }

        /// <summary>
        /// Given a target, will turn to face that target.
        /// </summary>
        /// <param name="target">The target to face.</param>
        protected virtual void FaceTarget(Transform target)
        {
            var facingVector = GetVectorXZToTarget(target);
            if (facingVector == Vector3.zero) return;
            Character.EntityTransform.rotation = Quaternion.LookRotation(facingVector);
        }
    }
}
