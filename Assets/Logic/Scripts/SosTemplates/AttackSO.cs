using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public LocalizedString attackName;
    public float physicalDamage;
    public float magicDamage;
}
