using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyBiomeListSO", menuName = "Enemies/EnemyBiomeListSO")]
public class EnemyBiomeListSO : ScriptableObject
{
    public enum EnemyRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [System.Serializable]
    public struct EnemyRarityData
    {
        public EnemySO enemySO;
        public EnemyRarity rarity;
    }

    public List<EnemyRarityData> enemyList;
}