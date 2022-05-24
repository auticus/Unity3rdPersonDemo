using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        private readonly List<Target> _targets = new();
        private const float DEFAULT_TARGETING_CAMERA_WEIGHT = 1f;
        private const float DEFAULT_TARGETING_CAMERA_RADIUS = 2f;

        [SerializeField] private CinemachineTargetGroup targetingGroup;

        public Target CurrentTarget { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Target target))
            {
                Debug.Log("Target was found!");
                target.OnDestroyed += RemoveTarget;
                _targets.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Target target))
            {
                RemoveTarget(target);
            }
        }

        /// <summary>
        /// Attempts to select the closest viable target.
        /// </summary>
        /// <returns>TRUE if able, FALSE otherwise.</returns>
        public bool TrySelectTarget()
        {
            //we want to select the target closest to what we're aiming at
            //currently is just grabbing the first one.
            if (!_targets.Any()) return false;
            CurrentTarget = _targets[0];
            targetingGroup.AddMember(CurrentTarget.transform, DEFAULT_TARGETING_CAMERA_WEIGHT, DEFAULT_TARGETING_CAMERA_RADIUS);
            return true;
        }

        /// <summary>
        /// Clears the <see cref="CurrentTarget"/>.
        /// </summary>
        public void ClearTarget()
        {
            if (CurrentTarget == null) return;
            targetingGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        
        private void RemoveTarget(Target target)
        {
            if (target == null) return;
            if (CurrentTarget == target)
            {
                ClearTarget();
            }

            target.OnDestroyed -= RemoveTarget;
            _targets.Remove(target);
        }
    }
}
