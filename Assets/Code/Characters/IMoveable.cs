using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    /// <summary>
    /// Interface applied to any object that houses data that can move via the <see cref="Locomotion"/> class.
    /// </summary>
    public interface IMoveable
    {
        Animator Animator { get; }
        CharacterController CharacterController { get; }
        float DefaultMovementSpeed { get; }
        Transform EntityTransform { get; }
        ForceReceiver Force { get; }
        float RotationDamping { get; }
    }
}
