using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion
{
    /// <summary>
    /// Interface applied to any object that houses data that can move via the <see cref="Locomotion"/> class.
    /// </summary>
    public interface IMoveable
    {
        Animator Animator { get; }
        CharacterController CharacterController { get; }
        float DefaultMovementSpeed { get; }

        /// <summary>
        /// Gets the transform of the object housing the <see cref="IMoveable"/>.
        /// </summary>
        Transform EntityTransform { get; }
        ForceReceiver Force { get; }
        float RotationDamping { get; }
    }
}
