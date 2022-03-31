using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData")]
public class GameInitData : ScriptableObject
{
    public int BallInitCount = 5;
    public float BallSpeed = 25;
}
