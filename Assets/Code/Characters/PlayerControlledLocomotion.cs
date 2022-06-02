using System.Collections.Generic;

namespace Unity3rdPersonDemo.Characters
{
    public class PlayerControlledLocomotion
    {
        private readonly Dictionary<LocomotionTypes, ILocomotion> _locomotionMap;

        public PlayerControlledLocomotion(IMoveable character)
        {
            _locomotionMap = new Dictionary<LocomotionTypes, ILocomotion>
            {
                { LocomotionTypes.FreeLook, new PlayerFreeLookLocomotion(character) },
                { LocomotionTypes.Targeting, new PlayerTargetingLocomotion(character) },
                { LocomotionTypes.Attacking, new PlayerAttackingLocomotion(character) }
            };
        }

        public void Process(LocomotionTypes locomotionType, float deltaTime)
            => _locomotionMap[locomotionType].Process(deltaTime);
    }
}
