using UnityEngine;

namespace mercenary_guild
{
    public class CombatCharacter : MonoBehaviour
    {
        public string CharacterName { get; protected set; }
        public float MaxHealth { get; protected set; }
        public float CurrentHealth { get; protected set; }
        public float PhysicalDamage { get; protected set; }
        public float MagicDamage { get; protected set; }
        public float PhysicalResistance { get; protected set; }
        public float MagicResistance { get; protected set; }
        protected void SetCharacterStats(string name, float maxHealth, float physicalDamage, float magicDamage, float physicalRes, float magicRes)
        {
            CharacterName = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            PhysicalDamage = physicalDamage;
            MagicDamage = magicDamage;
            PhysicalResistance = Mathf.Clamp01(physicalRes);
            MagicResistance = Mathf.Clamp01(magicRes);
        }

        public virtual void DealDamage(CombatCharacter target, float rawDamage, DamageType damageType)
        {
            if (target == null) return;

            Debug.Log($"{CharacterName} attacks {target.CharacterName} for {rawDamage} raw {damageType} damage!");
            target.RecieveDamage(rawDamage, damageType);
        }

        public virtual void RecieveDamage(float incomingDamage, DamageType damageType)
        {
            float resistance = damageType switch
            {
                DamageType.Physical => PhysicalResistance,
                DamageType.Magic => MagicResistance,
                DamageType.True => 0f,
                _ => 0f  //default
            };

            float finalDamage = incomingDamage * (1f - resistance);
            CurrentHealth = Mathf.Max(CurrentHealth - finalDamage, 0f);

            Debug.Log($"{CharacterName} took {finalDamage} of {damageType} damage. HP left: {CurrentHealth}/{MaxHealth}");

            if (CurrentHealth <= 0f)
            {
                Die();
            }
        }

        protected virtual void Die() //always loving the function DIE
        {
            Debug.Log($"{CharacterName} has been defeated!");
        }
    }

    public enum DamageType
    {
        Physical,
        Magic,
        True // Ignores resistances
    }
}