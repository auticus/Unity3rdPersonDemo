using System.Collections.Generic;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents the currently equipped weapon of the character and is a component of the Player object itself that bridges the weapon with the player.
    /// </summary>
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] [Tooltip("The currently equipped weapon")] private Weapon weapon;
        
        private GameObject weaponCollider;

        private void Start()
        {
            weaponCollider = weapon.gameObject;
        }

        public void EnableWeapon()
        {
            //called by the animator
            weapon.ClearItemsAlreadyHit();
            weaponCollider.SetActive(true);
        }

        public void DisableWeapon()
        {
            //called by the animator
            weaponCollider.SetActive(false);
        }

        /// <summary>
        /// Sets the multiplier for any damage done based on the animation.
        /// </summary>
        /// <param name="damageMultiplier"></param>
        public void SetAnimationDamageMultiplier(float damageMultiplier)
        {
            weapon.SetDamageMultiplier(damageMultiplier);
        }
    }
}
