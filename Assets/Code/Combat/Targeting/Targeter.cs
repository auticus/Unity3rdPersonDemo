using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        private readonly List<Target> _targets = new();
        private Camera _mainCamera;

        private const float DEFAULT_TARGETING_CAMERA_WEIGHT = 1f;
        private const float DEFAULT_TARGETING_CAMERA_RADIUS = 2f;

        [SerializeField] private CinemachineTargetGroup targetingGroup;

        public Target CurrentTarget { get; private set; }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Target target))
            {
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

            //cycle through each target and pull the target that is closest to center of screen
            Target bestPotentialTarget = null;
            var bestTargetPosition = Mathf.Infinity; //set it to the largest possible distance it can be

            foreach (var target in _targets)
            {
                //make sure x and y are between 0 & 1.  z doesn't matter because the range sphere on the player controller will keep things in a sane range
                var targetViewPosition = _mainCamera.WorldToViewportPoint(target.transform.position);
                if (targetViewPosition.x is < 0 or > 1) continue;
                if (targetViewPosition.y is < 0 or > 1) continue;

                var distanceFromCenter = GetDistanceFromCenterScreen(targetViewPosition);
                if (distanceFromCenter.sqrMagnitude < bestTargetPosition)
                {
                    bestPotentialTarget = target;
                    bestTargetPosition = distanceFromCenter.sqrMagnitude;
                }
            }

            ClearTarget();
            CurrentTarget = bestPotentialTarget;
            if (CurrentTarget is null) return false;
            
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

        private Vector2 GetDistanceFromCenterScreen(Vector3 targetViewPosition)
        {
            const float CENTER_SCREEN = 0.5f;
            var xDistanceFromCenter = Math.Abs(targetViewPosition.x - CENTER_SCREEN);
            var yDistanceFromCenter = Math.Abs(targetViewPosition.y - CENTER_SCREEN);
            return new Vector2(xDistanceFromCenter, yDistanceFromCenter);
        }
    }
}
