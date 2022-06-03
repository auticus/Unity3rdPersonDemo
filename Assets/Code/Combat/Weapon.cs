using System.Collections.Generic;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents a weapon.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField][Tooltip("The base damage of the weapon")] private int baseDamage;
        [SerializeField] private Collider owningCharacter;

        private readonly List<Collider> _itemsAlreadyHit = new();
        private float _characterAnimationDamageMultiplier = 1f;  //animation states may cause more or less damage

        private void OnTriggerEnter(Collider other)
        {
            if (other == owningCharacter) return;
            if (_itemsAlreadyHit.Contains(other)) return;

            //This should trigger when the collider on the weapon collides with a rigid body
            other.gameObject.TryGetComponent(out Health health);

            if (health == null)
            {
                return; //not something you can damage
            }
            health.DamageHealth((int)Mathf.Ceil(baseDamage * _characterAnimationDamageMultiplier));
            _itemsAlreadyHit.Add(other);
        }

        /// <summary>
        /// Sets the weapon damage multiplier which will multiply the damage of the weapon by the value passed.
        /// </summary>
        /// <param name="multiplier"></param>
        public void SetDamageMultiplier(float multiplier)
        {
            if (multiplier == 0) multiplier = 1;
            _characterAnimationDamageMultiplier = multiplier;
        }

        public void ClearItemsAlreadyHit() => _itemsAlreadyHit.Clear();
    }
}
