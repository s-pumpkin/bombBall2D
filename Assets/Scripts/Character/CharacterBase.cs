using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public CharacterData Data;
    public HpBar hpBar;
    public int Hp;

    public bool isDeath = false;
    public AnimEvent _animEvent;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        Hp = Data.HpMax;
        hpBar.SetValue(Data.HpMax);
    }

    public virtual void Update()
    {

    }

    public virtual void OnHurt(int damage)
    {
        if (isDeath)
            return;

        Hp -= damage;
        Hp = Mathf.Clamp(Hp, 0, Data.HpMax);
        hpBar.HpBarUpdate(Hp);
    }
}
