using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Camp { Player, Enemy }
public enum AttackMode { Easy, Long }

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public Camp MyCamp = Camp.Player;
    public AttackMode attackMode = AttackMode.Long;
    public int HpMax = 500;
    public int AttackBase = 50;

    public string DieAnimName;
    public string HitAnimName;
    public string EasyAttackAnimName;
    public string LongAttackAnimName;
    public string MoveAnimName;
}
