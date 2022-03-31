using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpBar : MonoBehaviour
{
    public Image m_HpBar;
    public TMP_Text m_HpText;

    private int HpMax;
    private int Hp;

    public void SetValue(int HpMax)
    {
        this.HpMax = HpMax;
        m_HpBar.fillAmount = 1;
        m_HpText.text = HpMax.NumberNuit();
    }

    public void HpBarUpdate(int currHp)
    {
        float percentage = currHp / (float)HpMax;
        m_HpBar.fillAmount = percentage;
        m_HpText.text = currHp.NumberNuit();
    }
}
