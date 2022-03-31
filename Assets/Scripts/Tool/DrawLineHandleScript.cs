using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawLineHandleScript : MonoBehaviour
{
    public int L = 10;
    public int H = 10;

    private void OnDrawGizmos()
    {
        for (int iL = 0; iL <= L; iL++)
        {
            Vector2 start = new Vector2(iL, 0), end = new Vector2(iL, H);
            Debug.DrawLine(start, end);
        }

        for (int iH = 0; iH <= H; iH++)
        {
            Vector2 start = new Vector2(0, iH), end = new Vector2(L, iH);
            Debug.DrawLine(start, end);
        }
    }
}
