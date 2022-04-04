using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum BattleState { UI, START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public GameInitData GameData;

    public BattleState NowState = BattleState.UI;
    public BattleState State
    {
        get
        {
            return NowState;
        }
        set
        {
            NowState = value;
            switch (value)
            {
                case BattleState.START:
                    StartCoroutine(OnLevelInit());
                    break;
                case BattleState.PLAYERTURN:
                    StartCoroutine(OnLevelPlayerTurn());
                    break;
                case BattleState.ENEMYTURN:
                    StartCoroutine(OnLevelEnemyTurn());
                    break;
                case BattleState.LOST:
                    StartCoroutine(OnLevelLOST());
                    break;
            }
        }
    }

    public List<IRemake> IRemakeList = new List<IRemake>();

    public AssetReference DefulBall;
    public AssetReference DefulEnemy;
    public GameObject m_BallPool;
    public GameObject m_EnemyPool;

    public ShowTextRef ComboText;
    //關卡中
    public int HitCombo = 0; //反彈總數
    public int BallHitEnemyComboAdd
    {
        set
        {
            if (value == -1)
                HitCombo = 0;
            else
                HitCombo += value;

            ComboText.RefText(HitCombo, true);
        }
    }

    public int BallCount;

    public int OnNewBallForID()
    {
        return BallCount = BallPool.BallCount() + 1;
    }



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        State = BattleState.START;
    }

    // Update is called once per frame
    void Update()
    {
        if (State == BattleState.PLAYERTURN && PlayerShootContral.Instance.UseBool)
        {
            if (BallPool.WorkBallPool.Count == 0)
            {
                State = BattleState.ENEMYTURN;
            }
        }
    }

    //遊戲剛開始初始化
    IEnumerator OnLevelInit()
    {
        ScreenManager.Instance.SetFadeIn();

        for (int i = 0; i < GameData.BallInitCount; i++)
        {
            BallPool.InstantiateNewBall(DefulBall, m_BallPool);
        }

        yield return new WaitForSeconds(2f);

        ScreenManager.Instance.SetFadeOut();
        State = BattleState.ENEMYTURN;
    }

    IEnumerator OnLevelEnemyTurn()
    {
        yield return new WaitForSeconds(2f);

        BallHitEnemyComboAdd = -1;

        //播放攻擊
        foreach (KeyValuePair<GameObject, EnemyContral> ec in EnemyPool.EnemyContralDictionary)
        {
            ec.Value.OnAttack();
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        //移動
        foreach (KeyValuePair<GameObject, EnemyContral> ec in EnemyPool.EnemyContralDictionary)
        {
            ec.Value.OnMove();
        }

        yield return new WaitForSeconds(1f);

        SummonEnemy(DefulEnemy);

        yield return new WaitForSeconds(2f);

        //重製項目
        foreach (var mode in IRemakeList)
            mode.OnRest();

        State = BattleState.PLAYERTURN;
    }

    public Vector2 CheckPosition(ref List<Vector2> V2List)
    {
        Vector2 v2;
        do
        {
            v2 = new Vector2(Random.Range(0, 6) + .5f, 6.5f);
        } while (V2List.Exists((x) => x == v2));

        V2List.Add(v2);
        return v2;
    }

    public void SummonEnemy(AssetReference enemy)
    {
        List<Vector2> CurrEnemyPositionList = new List<Vector2>();
        foreach (EnemyContral ec in EnemyPool.EnemyContralDictionary.Values)
        {
            CurrEnemyPositionList.Add(ec.transform.position);
        }


        int summonAmount = Random.Range(0, 5);
        for (int i = 0; i <= summonAmount; i++)
        {
            List<Vector2> listFindAll = CurrEnemyPositionList.FindAll((x) => x.y == 6.5f);
            if (listFindAll.Count == 6)
                return;

            Vector2 v2 = CheckPosition(ref CurrEnemyPositionList);
            enemy.InstantiateAsync(v2, Quaternion.identity, m_EnemyPool.transform);
        }
    }


    IEnumerator OnLevelPlayerTurn()
    {
        if (PlayerShootContral.Instance.Hp <= 0)
        {
            State = BattleState.LOST;
            yield break;
        }

        yield return new WaitForSeconds(2f);
    }

    IEnumerator OnLevelLOST()
    {

        yield return null;
    }
}