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
                enemyData.physicalDamage,
                enemyData.magicDamage,
                enemyData.physicalResistance,
                enemyData.magicResistance
            );
        }
        
        public int GetGoldReward() => enemyData != null ? enemyData.goldReward : 0;

        public override bool Attack(CombatCharacter target)
        {
            return DealDamage(target, PhysicalDamage, DamageTypeEnum.Physical);
        }
    }
}