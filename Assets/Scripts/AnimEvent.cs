using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    public Animator Anim;

    #region 動畫控制
    /// <summary>
    /// 動畫播完執行event
    /// </summary>
    /// <param name="AnimName"></param>
    /// <param name="Event"></param>
    public void SetPlayAnimEvent(string AnimName, Action Event = null)
    {
        if (string.IsNullOrEmpty(AnimName))
            return;
        Anim.Play(AnimName);
        StartCoroutine(OnAnimFinsh(Anim.GetCurrentAnimatorStateInfo(0).length, Event));
    }

    private IEnumerator OnAnimFinsh(float delay = 0f, Action e = null)
    {
        yield return new WaitForSeconds(delay);
        e?.Invoke();
    }
    #endregion

    public float GetAnimTimeLength()
    {
        return Anim.GetCurrentAnimatorStateInfo(0).length;
    }
}
