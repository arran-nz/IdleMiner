using System;

/// <summary>
/// Attribtute for Areas
/// </summary>
/// <typeparam name="T">Value Type</typeparam>
public class AreaAttribute<T>
{
    /// <summary>
    /// Display Name used for Upgrade Panel
    /// </summary>
    public string DisplayName;

    /// <summary>
    /// Desired String Formatting Method
    /// </summary>
    public Func<T, string> StringFormatMethod;

    /// <summary>
    /// Upgrade Method
    /// </summary>
    public Func<T, T> UpgradeMethod;

    public T Value;

    /// <summary>
    /// Return the value formatted for display
    /// </summary>
    /// <returns></returns>
    public string GetDisplayString()
    {
        return StringFormatMethod.Invoke(Value);
    }

    /// <summary>
    /// Get the upgrade amount
    /// </summary>
    /// <returns></returns>
    public T GetUpgradeAmount()
    {
        return UpgradeMethod(Value);
    }
}