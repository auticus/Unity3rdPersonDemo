﻿using System.Collections.Generic;
using Assets.Code.Locomotion.Player;

namespace Unity3rdPersonDemo.Locomotion.Player
{
    public class PlayerControlledLocomotion
    {
        private readonly Dictionary<LocomotionTypes, ILocomotion> _locomotionMap;

        public PlayerControlledLocomotion(IPlayerMoveable character)
        {
            _locomotionMap = new Dictionary<LocomotionTypes, ILocomotion>
            {
                { LocomotionTypes.FreeLook, new PlayerFreeLookLocomotion(character) },
                { LocomotionTypes.Targeting, new PlayerTargetingLocomotion(character) },
                { LocomotionTypes.Attacking, new PlayerAttackingLocomotion(character) },
                { LocomotionTypes.ImpactResponse, new PlayerImpactedLocomotion(character)}
            };
        }

        public void Process(LocomotionTypes locomotionType, float deltaTime)
            => _locomotionMap[locomotionType].Process(deltaTime);
    }
}
