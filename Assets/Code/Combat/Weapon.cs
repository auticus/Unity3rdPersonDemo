using System.Collections.Generic;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents the currently equipped weapon of the character and is a component of the Player object itself.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Collider owningCharacter;
        [SerializeField] private GameObject weaponCollider;
        [SerializeField] [Tooltip("The base damage of the weapon")] private int baseDamage;
        
        private readonly List<Collider> _itemsAlreadyHit = new();
        
        public void EnableWeapon()
        {
            //called by the animator
            _itemsAlreadyHit.Clear();
            weaponCollider.SetActive(true);
        }

        public void DisableWeapon()
        {
            //called by the animator
            weaponCollider.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == owningCharacter) return;
            if (_itemsAlreadyHit.Contains(other)) return;

            Debug.Log($"Entered target {other.gameObject.name}");
            //This should trigger when the collider on the weapon collides with a rigid body
            other.gameObject.TryGetComponent(out Health health);

            if (health == null)
            {
                return; //not something you can damage
            }
            health.DamageHealth(baseDamage);
            _itemsAlreadyHit.Add(other);
        }
    }
}
