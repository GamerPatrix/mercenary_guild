using System.Collections.Generic;
using UnityEngine;

namespace mercenary_guild.sos
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "GuildItems/WeaponSO")]
    public class WeaponSO : GearSO
    {
        List<AttackSO> attacks = new List<AttackSO>();
    }
}