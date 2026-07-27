using UnityEngine;
using System;

namespace mercenary_guild
{
    public abstract class CombatCharacter : MonoBehaviour
    {
        public event EventHandler OnCharacterDeath;
        public event Action<float> OnHealthChange;
        private float _maxHealth;
        private float _currentHealth;
        private float _physicalResistance;
        private float _magicResistance;
     
        public virtual float MaxHealth { get => _maxHealth; protected set => _maxHealth = value; }
        public virtual float CurrentHealth { get => _currentHealth; protected set => _currentHealth = value; }
        public virtual float PhysicalResistance { get => _physicalResistance; protected set => _physicalResistance = value; }
        public virtual float MagicResistance { get => _magicResistance; protected set => _magicResistance = value; }

        public abstract string GetDisplayName();
        protected void SetCharacterStats(float maxHealth, float physicalRes, float magicRes)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            PhysicalResistance = Mathf.Clamp01(physicalRes);
            MagicResistance = Mathf.Clamp01(magicRes);
        }

        public abstract bool Attack(CombatCharacter target);

        //returns true if the target was killed, false otherwise
        protected virtual bool DealDamage(CombatCharacter target, float rawDamage, DamageTypeEnum damageType)
        {
            if (target == null) return false;
            Debug.Log($"{GetDisplayName()} attacks {target.GetDisplayName()} for {rawDamage} raw {damageType} damage!");
            return target.RecieveDamage(rawDamage, damageType);
        }

        protected virtual bool RecieveDamage(float incomingDamage, DamageTypeEnum damageType)
        {
            float resistance = damageType switch
            {
                DamageTypeEnum.Physical => PhysicalResistance,
                DamageTypeEnum.Magic => MagicResistance,
                DamageTypeEnum.True => 0f,
                _ => 0f
            };

            float finalDamage = incomingDamage * (1f - resistance);
            CurrentHealth = Mathf.Max(CurrentHealth - finalDamage, 0f);

            Debug.Log($"{GetDisplayName()} took {finalDamage} of {damageType} damage. HP left: {CurrentHealth}/{MaxHealth}");
            OnHealthChange?.Invoke(CurrentHealth);
            if (CurrentHealth <= 0f)
            { 
                Die();
                return true;
            }
            return false;
        }

        protected virtual void Die()
        {
            Debug.Log($"{GetDisplayName()} has been defeated!");
            OnCharacterDeath?.Invoke(this, EventArgs.Empty);
        }
    }
}