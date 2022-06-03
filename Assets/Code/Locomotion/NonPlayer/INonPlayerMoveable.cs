using UnityEngine;
using UnityEngine.AI;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    public interface INonPlayerMoveable : IMoveable
    {
        GameObject Player { get; }
        NPCControlledLocomotion Locomotion { get; }
        NavMeshAgent NavAgent { get; }
    }
}
