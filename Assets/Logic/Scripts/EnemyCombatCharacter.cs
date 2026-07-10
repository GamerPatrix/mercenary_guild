using UnityEngine;

namespace mercenary_guild
{
    public class EnemyCombatCharacter : CombatCharacter
    {
        private EnemySO enemyData;

        public override string GetDisplayName()
        {
            return enemyData?.GetLocalizedEnemyName();
        }

        public void Initialize(EnemySO enemyData)
        {
            this.enemyData = enemyData;

            if (enemyData == null)
            {
                return;
            }

            SetCharacterStats(
                enemyData.maxHealth,
                enemyData.physicalResistance,
                enemyData.magicResistance
            );
        }
        
        public int GetGoldReward() => enemyData != null ? enemyData.goldReward : 0;

        public override bool Attack(CombatCharacter target)
        {

            var attack = enemyData.getRandomAttackSO();
            if(attack == null) return false;
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
    }
}