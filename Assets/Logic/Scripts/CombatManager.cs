using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
namespace mercenary_guild {
    //TODO refactor split to CombatManager & CombatUI
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private EnemyBiomeListSO m_enemyBiomeList;



        public static CombatManager instance { get; private set; }

        [SerializeField]
        private PlayerCombatCharacter playerCombatCharacter;
        [SerializeField]
        private EnemyCombatCharacter targetCombatCharacter;

        [SerializeField]
        private GameObject deathUI;
        [SerializeField]
        private GameObject combatUI;

        [SerializeField]
        private GameObject standardCombat;
        [SerializeField]
        private GameObject timeBasedDodge;
        private void Awake()
        {
            combatUI.SetActive(true);
            deathUI.SetActive(false);
            if (instance == null) instance = this;
            else Destroy(gameObject);
            targetCombatCharacter.Initialize(GetRandomEnemyByWeightedRarity());
        }

        private void Start()
        {
            playerCombatCharacter.OnCharacterDeath += PlayerCombatCharacter_OnCharacterDeath;
            targetCombatCharacter.OnCharacterDeath += TargetCombatCharacter_OnCharacterDeath;

            var a = timeBasedDodge.GetComponent<TimedButtonPressed>();
            a.OnClick += TimedButton_OnClick;
            standardCombat.SetActive(true);
            timeBasedDodge.SetActive(false);

            
        }

        
        public List<EnemySO> GetEnemiesByRarity(EnemyBiomeListSO.EnemyRarity targetRarity)
        {
            if (m_enemyBiomeList == null || m_enemyBiomeList.enemyList == null) return new List<EnemySO>();

            List<EnemySO> filteredEnemies = new List<EnemySO>();
            foreach (var data in m_enemyBiomeList.enemyList)
            {
                if (data.rarity == targetRarity)
                {
                    filteredEnemies.Add(data.enemySO);
                }
            }
            return filteredEnemies;
        }

        public EnemySO GetRandomEnemyByWeightedRarity()
        {
            if (m_enemyBiomeList == null || m_enemyBiomeList.enemyList == null || m_enemyBiomeList.enemyList.Count == 0)
            {
                return null;
            }

            Dictionary<EnemyBiomeListSO.EnemyRarity, float> rarityWeights = new Dictionary<EnemyBiomeListSO.EnemyRarity, float>()
            {
                { EnemyBiomeListSO.EnemyRarity.Common, 50f },
                { EnemyBiomeListSO.EnemyRarity.Uncommon, 30f },
                { EnemyBiomeListSO.EnemyRarity.Rare, 13f },
                { EnemyBiomeListSO.EnemyRarity.Epic, 6f },
                { EnemyBiomeListSO.EnemyRarity.Legendary, 1f }
            };

            float randomRoll = Random.Range(0f, 100f);
            float cumulativeWeight = 0f;
            EnemyBiomeListSO.EnemyRarity chosenRarity = EnemyBiomeListSO.EnemyRarity.Common;

            foreach (var kvp in rarityWeights)
            {
                cumulativeWeight += kvp.Value;
                if (randomRoll <= cumulativeWeight)
                {
                    chosenRarity = kvp.Key;
                    break;
                }
            }

            List<EnemySO> validEnemies = GetEnemiesByRarity(chosenRarity);

            if (validEnemies.Count == 0)
            {
                Debug.LogWarning($"No enemies found for chosen rarity: {chosenRarity}. Selecting from total pool.");
                int randomIndex = Random.Range(0, m_enemyBiomeList.enemyList.Count);
                return m_enemyBiomeList.enemyList[randomIndex].enemySO;
            }

            return validEnemies[Random.Range(0, validEnemies.Count)];
        }

        private void TimedButton_OnClick(int obj)
        {
            
            timeBasedDodge.GetComponent<TimedButtonPressed>().ResetPosition();
            timeBasedDodge.GetComponent<TimedButtonPressed>().StartMoving();
            timeBasedDodge.SetActive(false);
        }

        private void OnDestroy()
        {
            playerCombatCharacter.OnCharacterDeath -= PlayerCombatCharacter_OnCharacterDeath;
            targetCombatCharacter.OnCharacterDeath -= TargetCombatCharacter_OnCharacterDeath;
        }
        private void TargetCombatCharacter_OnCharacterDeath(object sender, System.EventArgs e)
        {
            Success();
        }

        private void PlayerCombatCharacter_OnCharacterDeath(object sender, System.EventArgs e)
        {
            Defeat();
        }

        private void Attack()
        {
             
        }

        public void PlayerWantsToAttackWith(AttackSO playerAttack)
        {
            if (playerCombatCharacter.playerAttackWrper(targetCombatCharacter, playerAttack)) return;
            targetCombatCharacter.Attack(playerCombatCharacter);
        }

        public bool Retreat()
        {
            Debug.Log("Retrieating yup englandos");
            Loader.LoadWLoading("FirstMap");
            return true;
        }

        public void Dodge()
        {
            timeBasedDodge.SetActive(true);
        }

        public void Success()
        {
            Debug.Log("SUccess he dead");
            Loader.LoadWLoading("FirstMap");
        }

        public void Defeat()
        {
            deathUI.SetActive(true);
            combatUI.SetActive(false);
        }
    }
}