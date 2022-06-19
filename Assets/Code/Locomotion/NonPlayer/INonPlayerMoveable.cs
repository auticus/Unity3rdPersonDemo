using UnityEngine;
using UnityEngine.AI;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    /// <summary>
    /// Interface applied to any NON PLAYER object that houses data that can move via the <see cref="Locomotion"/> class.
    /// </summary>
    public interface INonPlayerMoveable : IMoveable
    {
        GameObject Player { get; }
        NPCControlledLocomotion Locomotion { get; }
        NavMeshAgent NavAgent { get; }
    }
}
