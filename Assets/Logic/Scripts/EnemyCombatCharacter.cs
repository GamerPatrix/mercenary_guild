using UnityEngine;

namespace mercenary_guild
{
    public class EnemyCombatCharacter : CombatCharacter
    {
        [SerializeField] private EnemySO enemyData;

        private void Awake()
        {
            SetCharacterStats(
                enemyData.enemyName,
                enemyData.maxHealth,
                enemyData.physicalDamage,
                enemyData.magicDamage,
                enemyData.physicalResistance,
                enemyData.magicResistance
            ); 
        }

        
        public int GetGoldReward() => enemyData != null ? enemyData.goldReward : 0;

        public override void Attack(CombatCharacter target)
        {
            DealDamage(target, PhysicalDamage, DamageType.Physical);
        }
    }
}