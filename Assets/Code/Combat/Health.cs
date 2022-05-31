using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        private int _health;

        /// <summary>
        /// The current health of the object.
        /// </summary>
        public int CurrentHealth => _health;

        private void Start()
        {
            _health = maxHealth;
        }

        /// <summary>
        /// Damages the component with the amount given.
        /// </summary>
        /// <param name="damage">The amount of damage to apply.</param>
        public void DamageHealth(int damage)
        {
            if (_health == 0) return;
            _health -= damage;
            if (_health < 0) _health = 0;

            Debug.Log($"Target is damaged and has {_health} remaining");
        }
    }
}
