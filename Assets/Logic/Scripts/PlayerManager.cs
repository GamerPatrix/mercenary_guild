using UnityEngine;
using System;

namespace mercenary_guild
{
    public class PlayerManager : MonoBehaviour
    {
        public event EventHandler OnPlayerDeath;
        public event EventHandler<float> OnHealthChanged;

        public string CharacterName { get; internal set; }
        public float MaxHealth { get; internal set; }
        public float CurrentHealth { get; private set; }
        public float PhysicalDamage { get; internal set; }
        public float MagicDamage { get; internal set; }
        public float PhysicalResistance { get; internal set; }
        public float MagicResistance { get; internal set; }

        public static PlayerManager instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            InitializeStats();
        }

        private void InitializeStats()
        {
            CharacterName = "Player";
            MaxHealth = 100f;
            CurrentHealth = MaxHealth;
            PhysicalDamage = 15f;
            MagicDamage = 15f;
            PhysicalResistance = 0.1f;
            MagicResistance = 0.05f;
        }

        public void SetHealth(float newHealth)
        {
            CurrentHealth = Mathf.Clamp(newHealth, 0f, MaxHealth);
            OnHealthChanged?.Invoke(this, CurrentHealth);
        }

        public void Die()
        {
            Debug.Log("Player has bedieded!");
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}