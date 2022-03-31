using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static Dictionary<GameObject, EnemyContral> EnemyContralDictionary = new Dictionary<GameObject, EnemyContral>();

    public static void Register(GameObject go, EnemyContral monster)
    {
        if (!EnemyContralDictionary.ContainsKey(go))
            EnemyContralDictionary.Add(go, monster);
    }

    public static void UnRegister(GameObject go)
    {
        if (EnemyContralDictionary.ContainsKey(go))
            EnemyContralDictionary.Remove(go);
    }

    public static EnemyContral GetMonsterContral(GameObject go)
    {
        EnemyContral mc = null;
        if (EnemyContralDictionary.ContainsKey(go))
            mc = EnemyContralDictionary[go];
        return mc;
    }
}
