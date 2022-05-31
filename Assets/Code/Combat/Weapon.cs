using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] [Tooltip("The collider of the weapon")] private GameObject weapon;

        public void EnableWeapon()
        {
            weapon.SetActive(true);
        }

        public void DisableWeapon()
        {
            weapon.SetActive(false);
        }
    }
}
