using Unity3rdPersonDemo.Combat.Targeting;
using Unity3rdPersonDemo.Input;
using UnityEngine;

namespace Unity3rdPersonDemo.Locomotion.Player
{
    public interface IPlayerMoveable : IMoveable
    {
        InputReader InputReader { get; }
        Transform MainCameraTransform { get; }
        Targeter ObjectTargeter { get; }
        float TargetingMovementSpeed { get; }
    }
}
