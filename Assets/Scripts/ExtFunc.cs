using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtFunc
{
    public static string NumberNuit(this int nuber)
    {
        if (nuber > Math.Pow(10, 9))
            return (nuber / Math.Pow(10, 9)).ToString("#0.0") + "G";
        if (nuber > Math.Pow(10, 6))
            return (nuber / Math.Pow(10, 6)).ToString("#0.0") + "M";
        if (nuber > Math.Pow(10, 4))
            return (nuber / Math.Pow(10, 3)).ToString("#0.0") + "K";
        return nuber.ToString();
    }
}
