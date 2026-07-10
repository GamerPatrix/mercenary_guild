using UnityEngine;

namespace mercenary_guild
{
    public class PlayerCombatCharacter : CombatCharacter
    {
        private PlayerManager playerManager;

        private void Start()
        {
            if (playerManager == null)
            {
                playerManager = PlayerManager.instance;
            }
        }

        public override float MaxHealth { get => playerManager.MaxHealth; protected set => playerManager.MaxHealth = value; }
        public override float PhysicalResistance { get => playerManager.PhysicalResistance; protected set => playerManager.PhysicalResistance = value; }
        public override float MagicResistance { get => playerManager.MagicResistance; protected set => playerManager.MagicResistance = value; }

        public override float CurrentHealth
        {
            get => playerManager.CurrentHealth;
            protected set => playerManager.SetHealth(value);
        }

        public bool playerAttackWrper(CombatCharacter target,AttackSO attack)
        { 
            if (attack == null) return false;
            var PhysicalDamage = attack.physicalDamage;
            var MagicDamage = attack.magicDamage;

            if (PhysicalDamage > 0)
            {
                if (DealDamage(target, PhysicalDamage, DamageTypeEnum.Physical)) return true;
            }

            if (MagicDamage > 0)
            {
                if (DealDamage(target, MagicDamage, DamageTypeEnum.Magic)) return true;
            }
            return false;
        }

        public override bool Attack(CombatCharacter target)
        {
            throw new System.NotImplementedException("Use playerAttackWrper instead of Attack for PlayerCombatCharacter.");
        }

        protected override void Die()
        {
            if (playerManager != null)
            {
                playerManager.Die();
            }
            base.Die();
        }

        public override string GetDisplayName()
        {
            return playerManager.CharacterName;
        }
    }
}