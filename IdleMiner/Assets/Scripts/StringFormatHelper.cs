/// <summary>
/// Singular class responsible for formatting strings
/// </summary>
public static class StringFormatter
{
    /// <summary>
    /// Returns a currency string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCurrencyString(decimal value)
    {
        string customString = value.ToString("C2");
        customString = customString.Remove(0, 1);
        return customString + "a";
    }

    /// <summary>
    /// Returns a uniform currency per second string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCurrencyPerSecondString(decimal value)
    {
        string currency = GetCurrencyString(value);
        return currency + " /s";
    }

    /// <summary>
    /// Returns a capacity string with currency formatting
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetCapacityString(decimal value)
    {
        return GetCurrencyString(value);
    }

    /// <summary>
    /// Returns a Movement string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetMovementString(decimal value)
    {
        return value.ToString("F2");
    }

    /// <summary>
    /// Removes decimal places for worker displays. (You can't have half a worker!)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetWorkersString(int value)
    {
        return value.ToString("F0");
    }
}
