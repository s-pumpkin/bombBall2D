using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "BallData", menuName = "Data/Ball")]
public class BallData : ScriptableObject
{
    public enum Camp { Player, Enemy }
    public Camp camp = Camp.Player;

    public GameObject Prefab;
    public string Name;
    public int Attack;
    public int AttackMagnification;

    public bool negative_state;


}
