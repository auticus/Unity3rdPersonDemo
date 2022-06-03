using System.Collections.Generic;

namespace Unity3rdPersonDemo.Characters.NonPlayer
{
    /// <summary>
    /// Locomotion class for NPCs.
    /// </summary>
    public class NPCControlledLocomotion
    {
        private readonly Dictionary<LocomotionTypes, ILocomotion> _locomotionMap;

        public NPCControlledLocomotion(IMoveable character)
        {
            _locomotionMap = new Dictionary<LocomotionTypes, ILocomotion>
            {
                { LocomotionTypes.FreeLook, new NPCDefaultLocomotion(character) }
            };
        }

        public void Process(LocomotionTypes locomotionType, float deltaTime)
            => _locomotionMap[locomotionType].Process(deltaTime);
    }
}
