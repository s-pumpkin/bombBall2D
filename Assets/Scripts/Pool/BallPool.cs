using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

public class BallPool : MonoBehaviour
{
    public static Dictionary<GameObject, BallContral> BallContralGameObjectDictionary = new Dictionary<GameObject, BallContral>();

    public static Queue<BallContral> StockBallPool = new Queue<BallContral>();
    public static List<BallContral> WorkBallPool = new List<BallContral>();

    public static void AddNewBall(GameObject go, BallContral ballctr)
    {
        if (!BallContralGameObjectDictionary.ContainsKey(go))
        {
            BallContralGameObjectDictionary.Add(go, ballctr);
            StockBallPool.Enqueue(ballctr);
        }
    }

    public static void RemoveBallToDictionary(GameObject go)
    {
        if (BallContralGameObjectDictionary.ContainsKey(go)) BallContralGameObjectDictionary.Remove(go);
    }

    /// <summary>
    /// 清除物件池
    /// </summary>
    public static void ClearPool()
    {
        BallContralGameObjectDictionary.Clear();
        StockBallPool.Clear();
        WorkBallPool.Clear();
    }

    public static BallContral GetValue(GameObject go)
    {
        return BallContralGameObjectDictionary[go];
    }

    /// <summary>
    /// 依照生成順序排列
    /// </summary>
    public static void Sort()
    {
        var newList = StockBallPool.ToList();
        newList.Sort((x, y) => x.ID.CompareTo(y.ID));

        StockBallPool.Clear();
        foreach (var a in newList)
            StockBallPool.Enqueue(a);
    }

    public static BallContral ReUse()
    {
        var Ball = StockBallPool.Dequeue();
        WorkBallPool.Add(Ball);
        Ball.gameObject.SetActive(true);
        return Ball;
    }

    /// <summary>
    /// 回收
    /// </summary>
    /// <param name="recovery"></param>
    public static void Recovery(BallContral recovery)
    {
        recovery.gameObject.SetActive(false);
        StockBallPool.Enqueue(recovery);
        WorkBallPool.Remove(recovery);
    }

    public static int BallCount()
    {
        return StockBallPool.Count;
    }

    public static void InstantiateNewBall(AssetReference go, GameObject parent)
    {
        go.InstantiateAsync(parent.transform).Completed += x => x.Result.SetActive(false);
    }

}
