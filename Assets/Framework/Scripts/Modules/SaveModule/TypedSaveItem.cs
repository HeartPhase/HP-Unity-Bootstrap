using System.Collections.Generic;

/// <summary>
/// �����͵�SaveItem���ࡣ
/// ����û��ʲô���ã��������ﵱ�����
/// </summary>
/// <typeparam name="T">CRTP������ֻ��Ҫдһ��Log������</typeparam>
/// <typeparam name="V">�������ݵ����͡�</typeparam>
[System.Obsolete("Typed SaveItem is not in use. NOT TESTED", false)]
public abstract class ISaveItem<T, V> where T : ISaveItem<T, V>
{
    protected void LogInvalidItem(string key)
    {
        DevUtils.Log($"Invalid {typeof(T).Name} # {key}", "SaveItem");
    }

    /// <summary>
    /// ���浵�ļ��е��ַ���ת��Ϊһ�Լ�ֵ��
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
    /// ��һ�Լ�ֵת��Ϊ���ڴ浵���ַ�����
    /// </summary>
    public virtual string Impl_Encode(KeyValuePair<string, V> data)
    {
        return data.Key + "|" + data.Value.ToString();
    }

    /// <summary>
    /// �ַ���ת��Ϊ����ֵ��
    /// </summary>
    public abstract bool TryParse(string _value, out V value);

    /// <summary>
    /// ÿ�����͵�Ĭ�����ݣ�����������ʱ���
    /// </summary>
    public abstract V DefaultValue { get; }
}

//����Ϊ�������͵ľ���ʵ�֡�

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
