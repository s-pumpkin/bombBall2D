using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTextRef : MonoBehaviour
{
    public TMP_Text Text;

    public void RefText(int value, bool effect = false)
    {
        Text.text = value.ToString();
        if (value == 0)
            Text.text = "";

        if (effect)
            StartCoroutine(TextFontSize(100, 130));

    }

    public IEnumerator TextFontSize(int currSize, int toSize)
    {
        Text.fontSize = currSize;
        while (Text.fontSize != toSize)
        {
            Text.fontSize += 1;

            yield return null;
        }
        yield return null;
    }
}
