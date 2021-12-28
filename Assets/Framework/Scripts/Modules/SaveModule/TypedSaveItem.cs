using System.Collections.Generic;

/// <summary>
/// 带类型的SaveItem基类。
/// 好像并没有什么卵用，留在这里当作纪念。
/// </summary>
/// <typeparam name="T">CRTP，这样只需要写一个Log方法。</typeparam>
/// <typeparam name="V">所存数据的类型。</typeparam>
[System.Obsolete("Typed SaveItem is not in use. NOT TESTED", false)]
public abstract class ISaveItem<T, V> where T : ISaveItem<T, V>
{
    protected void LogInvalidItem(string key)
    {
        DevUtils.Log($"Invalid {typeof(T).Name} # {key}", "SaveItem");
    }

    /// <summary>
    /// 将存档文件中的字符串转换为一对键值。
    /// </summary>
    public virtual KeyValuePair<string, V> Impl_Decode(string save)
    {
        string[] segments = save.Split(FrameworkGlobals.SAVEFILE_DELIMITER);
        if (TryParse(segments[1], out V value))
        {
            return new KeyValuePair<string, V>(segments[0], value);
        }
        else
        {
            LogInvalidItem(segments[0]);
            return new KeyValuePair<string, V>(segments[0], DefaultValue);
        }

    }

    /// <summary>
    /// 将一对键值转换为用于存档的字符串。
    /// </summary>
    public virtual string Impl_Encode(KeyValuePair<string, V> data)
    {
        return data.Key + "|" + data.Value.ToString();
    }

    /// <summary>
    /// 字符串转换为数据值。
    /// </summary>
    public abstract bool TryParse(string _value, out V value);

    /// <summary>
    /// 每个类型的默认数据，用于数据损坏时填补。
    /// </summary>
    public abstract V DefaultValue { get; }
}

//以下为各个类型的具体实现。

[System.Obsolete("Typed SaveItem is not in use.", false)]
public class IntSaveItem : ISaveItem<IntSaveItem, int>
{
    public override bool TryParse(string _value, out int value)
    {
        return int.TryParse(_value, out value);
    }

    public override int DefaultValue => 0;
}

[System.Obsolete("Typed SaveItem is not in use.", false)]
public class FloatSaveItem : ISaveItem<FloatSaveItem, float>
{
    public override bool TryParse(string _value, out float value)
    {
        return float.TryParse(_value, out value);
    }

    public override float DefaultValue => 0.0f;
}

[System.Obsolete("Typed SaveItem is not in use.", false)]
public class BoolSaveItem : ISaveItem<BoolSaveItem, bool>
{
    public override bool TryParse(string _value, out bool value)
    {
        return bool.TryParse(_value, out value);
    }

    public override bool DefaultValue => false;
}

[System.Obsolete("Typed SaveItem is not in use.", false)]
public class StringSaveItem : ISaveItem<StringSaveItem, string>
{
    public override bool TryParse(string _value, out string value)
    {
        value = _value;
        if (string.IsNullOrEmpty(_value)) return false;
        return true;
    }

    public override string DefaultValue => "Default";
}
