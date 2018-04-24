using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatHelper {

	// Use this for initialization
	public static string GetCurrencyString (float value) {

        string customString = value.ToString("F2");
        if (customString.EndsWith("00"))
        {
            customString = customString.Substring(0, customString.Length - 3);
        }

        return customString + "a";

    }

}
