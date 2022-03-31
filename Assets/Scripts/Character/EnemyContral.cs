using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;


public class EnemyContral : CharacterBase, IRemake
{
    public Collider2D col2D;

    public override void Start()
    {
        base.Start();
        EnemyPool.Register(gameObject, this);
    }

    public override void OnHurt(int damage)
    {
        base.OnHurt(damage);

        if (Hp <= 0)
        {
            isDeath = true;
            col2D.enabled = false;

            EnemyPool.UnRegister(gameObject);
            _animEvent.SetPlayAnimEvent(Data.DieAnimName, () => Destroy(gameObject));
        }
    }



    /// <summary>
    /// IRemake
    /// </summary>
    public void OnRest()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// 又修改 新增攻擊
    /// </summary>
    public void OnAttack()
    {
        if (Data.attackMode == AttackMode.Easy)
        {
            if (transform.position.y != .5f)
                return;

            _animEvent.SetPlayAnimEvent(Data.EasyAttackAnimName, OnEasyAttack);
        }

        if (Data.attackMode == AttackMode.Long)
            _animEvent.SetPlayAnimEvent(Data.LongAttackAnimName);
    }

    //要改 攻擊後會出問題
    public void OnEasyAttack()
    {
        PlayerShootContral.Instance.OnHurt(Data.AttackBase);
        OnHurt(Data.HpMax);
    }

    public void OnMove()
    {
        _animEvent.SetPlayAnimEvent(Data.MoveAnimName);
        transform.DOMoveY(transform.position.y - 1, _animEvent.GetAnimTimeLength());
    }
}
