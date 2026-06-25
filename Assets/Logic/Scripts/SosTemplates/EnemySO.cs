using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy Data")]
public class EnemySO : ScriptableObject
{
    [Header("Base Visuals")]
    [Tooltip("The actual visual prefab spawned into the world")]
    public GameObject enemyPrefab;
    public string enemyName;

    [Header("Core Stats")]
    public float maxHealth;
    public float physicalDamage;
    public float magicDamage;

    [Header("Resistances (0.0 = full damage, 0.5 = 50% reduction, 1.0 = immune)")]
    [Range(0f, 1f)] public float physicalResistance;
    [Range(0f, 1f)] public float magicResistance;

    [Header("Rewards")]
    public int experienceReward;
    public int goldReward;
}