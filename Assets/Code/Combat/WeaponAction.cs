using System;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    /// <summary>
    /// Represents an action that the weapon can take.  This is attached to the collider of each weapon.
    /// </summary>
    public class WeaponAction : MonoBehaviour
    {
        public event Action OnWeaponSwinging;

        private void OnEnable()
        {
            OnWeaponSwinging?.Invoke();
        }
    }
}
