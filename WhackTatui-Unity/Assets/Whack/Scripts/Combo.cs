using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public static class Combo
{
    private static float mult;
    public static float Multiplicador
    {
        get
        {
            if (mult == 0) mult = 1;
            return mult;
        }
        private set
        {
            mult = value;
        }
    }

    public static void Aumentar()
    {
        mult += mult / 10;
        if (mult > 10)
        {
            mult = 10;
        }
    }

    public static void Set()
    {
        Multiplicador = 1;
    }
}
