using UnityEngine;

namespace mercenary_guild
{
    public class PlayerCombatCharacter : CombatCharacter
    {
        private void Start()
        {
            LoadPlayerStats();
        }

        private void LoadPlayerStats()
        {

            SetCharacterStats(
                name: "Hero",
                maxHealth: 100f,
                physicalDamage: 15f,
                magicDamage: 15f,
                physicalRes: 0.1f, // 10% physical reduction
                magicRes: 0.05f    // 5% magic reduction
            );
        }


        protected override void Die()
        {
            base.Die();
            Debug.Log("Triggering Game Over Screen UI...");
        }
    }
}