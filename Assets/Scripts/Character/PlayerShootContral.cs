using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerShootContral : CharacterBase, IRemake
{
    private static PlayerShootContral instance;
    public static PlayerShootContral Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerShootContral>();
            return instance;
        }
    }

    //LinRenderer
    public GameObject m_LineGameObject;
    public LineRenderer m_LineRenderer;
    public GameObject m_Aim;

    public ShowTextRef ShowBallCountText;
    public bool UseBool;
    private bool Ispress;

    private float BalltoDirDistance;
    private Vector3 NewDir;
    public Vector3 mouseDir;

    public LayerMask LineLayerMask;
    public override void Awake()
    {
        instance = this;
        GameManager.Instance.IRemakeList.Add(this);
    }

    public override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.State != BattleState.PLAYERTURN || UseBool)
            return;

        LineUpdate();
    }

    public override void Update()
    {
        if (GameManager.Instance.State != BattleState.PLAYERTURN || UseBool)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ispress = true;
            m_LineGameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            BalltoDirDistance = Vector3.Distance(mousePos, transform.position);

            var OldDir = mousePos - transform.position;
            NewDir.x = Mathf.Clamp(OldDir.x, -3f, 3f) / BalltoDirDistance;
            NewDir.y = Mathf.Clamp(OldDir.y, 1.1f, 15f) / BalltoDirDistance;

            mouseDir = NewDir.normalized;
        }

        if (Input.GetMouseButtonUp(0) && Ispress)
        {
            Ispress = false;
            UseBool = true;
            m_LineGameObject.SetActive(false);
            StartCoroutine(ShootBall());
        }
    }

    /// <summary>
    /// Æg¿ªΩuUpdate
    /// </summary>
    public void LineUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .1f, mouseDir, Mathf.Infinity, LineLayerMask);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseDir, Mathf.Infinity, LineLayerMask);

        if (!hit)
            return;

        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, hit.point);

        m_Aim.transform.position = hit.point;
    }

    //≠nßÔ
    IEnumerator ShootBall()
    {
        int currBall = GameManager.Instance.BallCount;
        while (currBall > 0)
        {
            currBall -= 1;

            var ball = BallPool.ReUse();
            ball.transform.position = transform.position;
            ball.transform.rotation = Quaternion.FromToRotation(Vector2.up, mouseDir);
            ball.rig2D.velocity = mouseDir * GameManager.Instance.GameData.BallSpeed;

            ShowBallCountText.RefText(currBall);
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }

    public void OnChangePosition(Vector3 pos)
    {
        transform.DOMoveX(pos.x, 0.5f);
    }

    public override void OnHurt(int damage)
    {
        base.OnHurt(damage);
    }

    /// <summary>
    /// IRemake
    /// </summary>
    public void OnRest()
    {
        UseBool = false;
        ShowBallCountText.RefText(GameManager.Instance.BallCount);
    }
}
