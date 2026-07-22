using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace mercenary_guild
{

    public class BottomCombatMenuUIManager : MonoBehaviour
    {

        [SerializeField] private BottomCombatMenuUI menuUI;
        [SerializeField] private List<LocalizedString> mainMenuStrings = new List<LocalizedString>();
        [SerializeField] private LocalizedString backButtonText = new LocalizedString();
        [SerializeField] private CombatManager combatManager;

        private BottomCombatMenuUI.MenuMode currentMode = BottomCombatMenuUI.MenuMode.Main;

        public void Awake()
        {      
            menuUI.Initialize(this);
        }

        public void Start()
        { 
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            currentMode = BottomCombatMenuUI.MenuMode.Main;
            menuUI.UpdateDisplay(mainMenuStrings, OnMainMenuItemClick, showBackButton: false);
        }

        public void ShowAttacksMenu()
        {
            currentMode = BottomCombatMenuUI.MenuMode.Attacks;
            if (PlayerManager.instance != null && PlayerManager.instance.attacks != null)
            {
                List<LocalizedString> attackStrings = new List<LocalizedString>();
                foreach (var attack in PlayerManager.instance.attacks)
                {
                    attackStrings.Add(attack.attackName);
                }
                menuUI.UpdateDisplay(attackStrings, OnAttackItemClick, showBackButton: true);
            }
        }

        public void ShowPotionsMenu()
        {
            currentMode = BottomCombatMenuUI.MenuMode.Potions;
            // For now, just show a placeholder
            List<LocalizedString> potionStrings = new List<LocalizedString>();
            menuUI.UpdateDisplay(potionStrings, OnPotionItemClick, showBackButton: true);
        }

        public void OnMainMenuItemClick(int id)
        {
            switch (id)
            {
                case 0: // Attack button
                    ShowAttacksMenu();
                    break;
                case 1: // Potions button
                    ShowPotionsMenu();
                    break;
                case 2: // Dodge button
                    // Trigger the combat manager dodge flow (shows the timed dodge UI)
                    if (combatManager != null) combatManager.Dodge();
                    else Debug.LogWarning("CombatManager reference is missing when attempting to Dodge");
                    break;
                case 3: // Retreat button
                    Retreat();
                    break;
            }
        }

        public void OnAttackItemClick(int id)
        {
            if (PlayerManager.instance != null && id >= 0 && id < PlayerManager.instance.attacks.Count)
            {
                var selectedAttack = PlayerManager.instance.attacks[id];
                
                combatManager.PlayerWantsToAttackWith(selectedAttack);
            }
            Debug.LogError("Somehow no attack selected");
        }

        public void OnPotionItemClick(int id)
        {
            // TODO: Implement potion selection logic
            Debug.Log($"Selected potion item {id}");
        }

        public void OnBackButtonClick()
        {
            // Return to main menu
            ShowMainMenu();
        }

        private void Retreat()
        {
            Debug.Log("Retreat button pressed");
            // TODO: just calls retreat on the combatManager so i dont think it needs its own method 
        }
    }
}
