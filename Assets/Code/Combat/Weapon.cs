using System.Collections.Generic;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents the currently equipped weapon of the character.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [SerializeField] [Tooltip("The collider of the weapon")] private GameObject weaponCollider;
        [SerializeField] private Collider owningCharacter;
        [SerializeField] private int baseDamage;
        
        private readonly List<Collider> _itemsAlreadyHit = new();

        private void Start()
        {
            weaponCollider.gameObject.TryGetComponent(out WeaponAction swing);
            swing.OnWeaponSwinging += OnWeaponSwinging;
        }

        private void OnDestroy()
        {
            weaponCollider.gameObject.TryGetComponent(out WeaponAction swing);
            swing.OnWeaponSwinging -= OnWeaponSwinging;
        }

        private void OnTriggerEnter(Collider target)
        {
            if (target == owningCharacter) return; //dont let me hit myself

            if (_itemsAlreadyHit.Contains(target)) return; //dont hit the same thing twice in one swing

            //This should trigger when the collider on the weapon collides with a rigid body
            target.gameObject.TryGetComponent(out Health health);
            if (health == null) return; //not something you can damage

            health.DamageHealth(baseDamage);
            _itemsAlreadyHit.Add(target);
        }

        public void EnableWeapon()
        {
            weaponCollider.SetActive(true);
        }

        public void DisableWeapon()
        {
            weaponCollider.SetActive(false);
        }

        private void OnWeaponSwinging()
        {
            //every time the weapon begins to swing, clear the items already hit list.
            _itemsAlreadyHit.Clear();
        }
    }
}
