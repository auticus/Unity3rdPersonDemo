using System;
using UnityEngine;

namespace Unity3rdPersonDemo.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        
        /// <summary>
        /// Gets the current health of the object.
        /// </summary>
        public int CurrentHealth { get; private set; }
        
        /// <summary>
        /// Gets or sets a percentage that indicates how much damage can be blocked.
        /// </summary>
        public float BlockPercentage { get; set; }

        public event Action OnDeath;

        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        /// <summary>
        /// Damages the component with the amount given.
        /// </summary>
        /// <param name="damage">The amount of damage to apply.</param>
        public void DamageHealth(int damage)
        {
            if (CurrentHealth == 0) return;

            if (BlockPercentage > 0)
            {
                damage = GetUnblockedDamage(damage);
            }

            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }

            Debug.Log($"Target is damaged and has {CurrentHealth} remaining");
        }

        private int GetUnblockedDamage(int damage)
            => damage - Mathf.CeilToInt(damage * BlockPercentage);
    }
}
