using UnityEngine;
using UnityEngine.UI;
namespace mercenary_guild {
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private Button attack;
        [SerializeField] private Button retreat;



        public static CombatManager instance { get; private set; }

        [SerializeField]
        private PlayerCombatCharacter playerCombatCharacter;
        [SerializeField]
        private EnemyCombatCharacter targetCombatCharacter;

        [SerializeField]
        private GameObject deathUI;
        [SerializeField]
        private GameObject combatUI;
        private void Awake()
        {
            combatUI.SetActive(true);
            deathUI.SetActive(false);
            if (instance == null) instance = this;
            else Destroy(gameObject);

            attack.onClick.AddListener(() => Attack()); 

            retreat.onClick.AddListener(() => Retreat());
        }

        private void Start()
        {
            playerCombatCharacter.OnCharacterDeath += PlayerCombatCharacter_OnCharacterDeath;
            targetCombatCharacter.OnCharacterDeath += TargetCombatCharacter_OnCharacterDeath;
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
            playerCombatCharacter.Attack(targetCombatCharacter);
            targetCombatCharacter.Attack(playerCombatCharacter);
        }

        public bool Retreat()
        {
            Debug.Log("Retrieating yup englandos");
            Loader.LoadWLoading("FirstMap");
            return true;
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