using System;

public class AreaAttribute<T>
{
    public string DisplayName;
    public Func<T, string> StringFormatMethod;
    public Func<T, T> UpgradeMethod;

    public T Value;

    public string GetDisplayString()
    {
        return StringFormatMethod.Invoke(Value);
    }

    public T GetUpgradeAmount()
    {
        return UpgradeMethod(Value);
    }
}