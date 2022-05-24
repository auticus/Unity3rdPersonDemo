using Unity3rdPersonDemo.Combat.Targeting;
using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    /// <summary>
    /// Class responsible for moving characters.
    /// </summary>
    public abstract class Locomotion
    {
        public const float DAMP_SMOOTH_TIME = 0.1f;
        public const int IDLE = 0;
        public const int RUNNING = 1;
        protected readonly IMoveableState Character;

        protected Locomotion(IMoveableState character)
        {
            Character = character;
        }

        /// <summary>
        /// Processes Locomotion based on the type of state passed to it. 
        /// </summary>
        /// <param name="deltaTime">The delta time tick that applies to the movement.</param>
        public abstract void Process<T>(float deltaTime);

        /// <summary>
        /// Given a target, will turn to face that target.
        /// </summary>
        /// <param name="target">The target to face.</param>
        public virtual void FaceTarget(Target target)
        {
            if (target == null) return;
            var facingVector = target.transform.position - Character.EntityTransform.position;
            facingVector.y = 0; //we don't care about the height difference
            Character.EntityTransform.rotation = Quaternion.LookRotation(facingVector);
        }
    }
}
