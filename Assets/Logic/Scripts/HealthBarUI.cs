using UnityEngine;
using UnityEngine.UI;

namespace mercenary_guild
{
    public class HealthBarUI : MonoBehaviour
    {
        private Slider healthSlider;
        [SerializeField] private CombatCharacter chara;
        private bool isInitialized = false;

        void Start()
        {
            healthSlider = GetComponent<Slider>();
            chara.OnHealthChange += Character_OnHealthChange;
        }

        void Update()
        {
            if (!isInitialized)
            {
                healthSlider.maxValue = chara.MaxHealth;
                healthSlider.value = chara.CurrentHealth; 
                isInitialized = true;

            }
        }

        private void Character_OnHealthChange(float obj)
        {
            healthSlider.value = obj;
            
            if (obj <= 0 )
                healthSlider.fillRect.gameObject.SetActive(false);  
            else 
                healthSlider.fillRect.gameObject.SetActive(true);

        }
        private void OnDestroy()
        {
            if (chara != null)
            {
                chara.OnHealthChange -= Character_OnHealthChange;
            }
        }
    }
}