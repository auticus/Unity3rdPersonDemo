using Unity3rdPersonDemo.Combat.Targeting;
using Unity3rdPersonDemo.Input;
using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion.Player
{
    /// <summary>
    /// Interface applied to any PLAYER object that houses data that can move via the <see cref="Locomotion"/> class.
    /// </summary>
    public interface IPlayerMoveable : IMoveable
    {
        InputReader InputReader { get; }
        Transform MainCameraTransform { get; }
        Targeter ObjectTargeter { get; }
        float TargetingMovementSpeed { get; }
    }
}
