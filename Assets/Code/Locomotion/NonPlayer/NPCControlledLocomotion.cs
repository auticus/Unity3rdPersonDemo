using System.Collections.Generic;

namespace Unity3rdPersonDemo.Locomotion.NonPlayer
{
    /// <summary>
    /// Locomotion class for NPCs.
    /// </summary>
    public class NPCControlledLocomotion
    {
        private readonly Dictionary<LocomotionTypes, ILocomotion> _locomotionMap;

        public NPCControlledLocomotion(INonPlayerMoveable character)
        {
            _locomotionMap = new Dictionary<LocomotionTypes, ILocomotion>
            {
                { LocomotionTypes.FreeLook, new NPCDefaultLocomotion(character) },
                { LocomotionTypes.Pursuit, new NPCPursuitLocomotion(character) },
                { LocomotionTypes.ImpactResponse, new NPCImpactedLocomotion(character)}
            };
        }

        public void Process(LocomotionTypes locomotionType, float deltaTime)
            => _locomotionMap[locomotionType].Process(deltaTime);
    }
}
