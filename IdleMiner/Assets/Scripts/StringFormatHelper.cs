using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatHelper {

	// Use this for initialization
	public static string GetCurrencyString (float value) {

        string customString = value.ToString("F2");
        return customString + " a";

    }

}
