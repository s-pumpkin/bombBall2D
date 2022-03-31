using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContral : MonoBehaviour
{
    public int ID { get; protected set; }
    public BallData data;

    public int Reflection = 0;
    public Rigidbody2D rig2D;
    public Vector2 lastVelocity;
    public float Speed;

    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        ID = GameManager.Instance.OnNewBallForID();
        BallPool.AddNewBall(this.gameObject, this);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AvoidStopSpeed();

        Speed = lastVelocity.magnitude;
    }

    /// <summary>
    ///避免球速度為0
    /// </summary>
    private void AvoidStopSpeed()
    {
        if (rig2D.velocity != Vector2.zero)
            lastVelocity = rig2D.velocity;
        else
            rig2D.velocity = -lastVelocity;
        //rig2D.velocity = lastVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyContral mc = EnemyPool.GetMonsterContral(collision.gameObject);
        if (mc)
        {
            mc.OnHurt(data.Attack);
            GameManager.Instance.BallHitEnemyComboAdd = 1;
        }

        BallReflection(collision);
    }

    private void BallReflection(Collision2D col)
    {
        Vector2 dir = Vector2.Reflect(lastVelocity.normalized, col.contacts[0].normal);
        transform.rotation = Quaternion.FromToRotation(Vector2.up, dir);

        Vector2 NewVelocity = dir / dir.magnitude * GameManager.Instance.GameData.BallSpeed;//固定球速度

        AvoidHorizontalReflection(ref NewVelocity);
        rig2D.velocity = NewVelocity;

        Reflection += 1;
    }

    private void AvoidHorizontalReflection(ref Vector2 v2)
    {
        float Sign = Mathf.Sign(v2.y);
        if (Mathf.Abs(v2.y) <= 1f)
            v2.y = Sign * 1f;
    }

    private void OnEnable()
    {
        Reflection = 0;
    }
}
