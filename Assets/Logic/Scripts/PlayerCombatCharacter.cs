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

        public override string CharacterName { get => playerManager.CharacterName; protected set => playerManager.CharacterName = value; }
        public override float MaxHealth { get => playerManager.MaxHealth; protected set => playerManager.MaxHealth = value; }
        public override float PhysicalDamage { get => playerManager.PhysicalDamage; protected set => playerManager.PhysicalDamage = value; }
        public override float MagicDamage { get => playerManager.MagicDamage; protected set => playerManager.MagicDamage = value; }
        public override float PhysicalResistance { get => playerManager.PhysicalResistance; protected set => playerManager.PhysicalResistance = value; }
        public override float MagicResistance { get => playerManager.MagicResistance; protected set => playerManager.MagicResistance = value; }

        public override float CurrentHealth
        {
            get => playerManager.CurrentHealth;
            protected set => playerManager.SetHealth(value);
        }

        public override void Attack(CombatCharacter target)
        {
            DealDamage(target, PhysicalDamage, DamageTypeEnum.Physical);
        }

        protected override void Die()
        {
            if (playerManager != null)
            {
                playerManager.Die();
            }
            base.Die();
        }
    }
}