using System.Collections.Generic;
using Unity3rdPersonDemo.Locomotion;
using Unity3rdPersonDemo.StateMachine;
using UnityEditor.Rendering;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents a weapon.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField][Tooltip("The base damage of the weapon")] private int baseDamage;
        [SerializeField][Tooltip("The base knockback value of the weapon")] private float baseKnockback;

        [SerializeField] private Collider owningCharacter;

        private readonly List<Collider> _itemsAlreadyHit = new();
        private float _characterAnimationDamageMultiplier = 1f;  //animation states may cause more or less damage
        private float _characterAnimationKnockbackMultiplier = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other == owningCharacter) return;
            if (_itemsAlreadyHit.Contains(other)) return;

            //This should trigger when the collider on the weapon collides with a rigid body
            other.gameObject.TryGetComponent(out Health health);
            other.gameObject.TryGetComponent(out ForceReceiver forceReceiver);
            other.gameObject.TryGetComponent(out IImpactable impactReceiver);

            if (health != null)
            {
                health.DamageHealth((int)Mathf.Ceil(baseDamage * _characterAnimationDamageMultiplier));
                _itemsAlreadyHit.Add(other);
            }

            if (forceReceiver != null)
            {
                var knockBackDirection = (other.transform.position - owningCharacter.transform.position).normalized;
                forceReceiver.AddForce(knockBackDirection * baseKnockback * _characterAnimationKnockbackMultiplier);
            }

            if (impactReceiver != null)
            {
                impactReceiver.PerformImpact();
            }
        }

        /// <summary>
        /// Sets the weapon damage multiplier and knockback multiplier which will multiply the damage or knockback of the weapon by the value passed.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        public void SetAnimationMultipliers(float damage, float knockback)
        {
            if (damage == 0) damage = 1;
            if (knockback == 0) knockback = 1;
            _characterAnimationDamageMultiplier = damage;
            _characterAnimationKnockbackMultiplier = knockback;
        }

        public void ClearItemsAlreadyHit() => _itemsAlreadyHit.Clear();
    }
}
