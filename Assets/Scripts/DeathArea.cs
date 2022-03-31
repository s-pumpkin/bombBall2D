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
        BallContral ballCtr = BallPool.GetValue(collision.gameObject);
        if (ballCtr == null)
        {
            Debug.LogError("�W��: " + ballCtr.name + "ID: " + ballCtr.ID + " ���y�����U");
            return;
        }

        if (ballCtr.Reflection == 0)
            return;

        if (!FristCol)
        {
            FristCol = true;
            PlayerShootContral.Instance.OnChangePosition(collision.gameObject.transform.position);
        }

        BallPool.Recovery(ballCtr);
    }
}
