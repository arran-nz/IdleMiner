using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatHelper
{
    public static string GetCurrencyString(decimal value)
    {
        string customString = value.ToString("F2");
        return customString + "a";
    }

    public static string GetCurrencyPerSecondString(decimal value)
    {
        string currency = GetCurrencyString(value);
        return currency + " /s";
    }

    public static string GetCapacityString(decimal value)
    {
        return value.ToString("F2");
    }

    public static string GetMovementString(decimal value)
    {
        return value.ToString("F2");
    }

    public static string GetWorkersString(int value)
    {
        return value.ToString("F0");
    }


}
