using UnityEngine;

namespace mercenary_guild {
    public class CombatManager : MonoBehaviour
    {

        public static CombatManager instance { get; private set; }

        [SerializeField]
        private PlayerCombatCharacter playerCombatCharacter;

        


        public bool Tryretreat()
        {
            Loader.LoadWLoading("FirstMap");
            return true;
        }

        public void Success()
        {
            Debug.Log("SUccess he dead");
        }

        public void Defeat()
        {
            Debug.Log("you pretty much dead");
        }
    }
}