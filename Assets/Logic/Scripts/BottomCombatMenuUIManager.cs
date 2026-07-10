using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace mercenary_guild
{

    public class BottomCombatMenuUIManager : MonoBehaviour
    {

        [SerializeField] private BottomCombatMenuUI menuUI;
        [SerializeField] private List<LocalizedString> localizedStrings = new List<LocalizedString>();

        public void Awake()
        {      
            menuUI.Initialize(this);
        }

        public void Start()
        { 
            menuUI.UpdateDisplay(localizedStrings);
        }

        public void OnItemClick(int id)
        {
            // Handle item click with the provided ID
            Debug.Log($"Item with ID {id} was pressed");
        }
    }
}
