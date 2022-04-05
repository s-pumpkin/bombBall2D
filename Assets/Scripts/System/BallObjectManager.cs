using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BallObjectManager : MonoBehaviour
{
    public AssetLabelReference m_BallLabel;
    public static Dictionary<string, GameObject> BallNameGameObjectDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        OnLoadBallObject();
    }

    private void Update()
    {
        Debug.Log(BallNameGameObjectDictionary.Count);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(BallNameGameObjectDictionary["BallDeful"]);
        }
    }

    public void OnLoadBallObject()
    {
        Addressables.LoadAssetsAsync<GameObject>(m_BallLabel, null).Completed += OnAssetObjLoaded;
    }

    public void OnAssetObjLoaded(AsyncOperationHandle<IList<GameObject>> objs)
    {
        foreach (var obj in objs.Result)
        {
            BallNameGameObjectDictionary.Add(obj.name, obj);
        }
    }

    public static void InstantiateBallGameObject(string name, GameObject parent)
    {
        GameObject newBall = Instantiate(BallNameGameObjectDictionary[name], parent.transform);
        newBall.SetActive(false);
    }
}
