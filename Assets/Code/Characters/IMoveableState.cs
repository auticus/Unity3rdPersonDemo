using Unity3rdPersonDemo.Combat.Targeting;
using Unity3rdPersonDemo.Input;
using UnityEngine;

namespace Unity3rdPersonDemo.Characters
{
    /// <summary>
    /// Interface applied to any object that houses data that can move via the <see cref="Locomotion"/> class.
    /// </summary>
    public interface IMoveableState
    {
        Animator Animator { get; }
        CharacterController CharacterController { get; }
        Transform EntityTransform { get; }
        ForceReceiver Force { get; }
        float FreeLookMovementSpeed { get; }
        float FreeLookRotationDamping { get; }
        InputReader InputReader { get; }
        Transform MainCameraTransform { get; }
        Targeter ObjectTargeter { get; }
        float TargetingMovementSpeed { get; }
    }
}
