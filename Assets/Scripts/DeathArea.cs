using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour, IRemake
{
    private bool FristCol = false;

    private void Start()
    {
        GameManager.Instance.IRemakeList.Add(this);
    }

    public void OnRest()
    {
        FristCol = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallRecoery(collision);
    }

    void BallRecoery(Collider2D col)
    {
        BallContral ballCtr = BallPool.GetValue(col.gameObject);
        if (ballCtr == null)
        {
            Debug.LogError("名稱: " + ballCtr.name + "ID: " + ballCtr.ID + " 此球未註冊");
            return;
        }

        if (ballCtr.Reflection == 0)
            return;

        if (!FristCol)
        {
            FristCol = true;
            PlayerShootContral.Instance.OnChangePosition(col.gameObject.transform.position);
        }

        BallPool.Recovery(ballCtr);
    }
}
