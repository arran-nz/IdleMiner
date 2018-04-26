using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringFormatHelper {

	// Use this for initialization
	public static string GetCurrencyString (decimal value) {

        string customString = value.ToString("F2");
        return customString + " a";

    }

}
