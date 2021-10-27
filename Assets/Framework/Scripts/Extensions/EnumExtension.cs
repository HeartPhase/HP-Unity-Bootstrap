using System.ComponentModel;
using System.Reflection;
using System;

/// <summary>
/// Enum����չ��
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// ��ȡEnum��ÿ��ö��ͷ��ָ��Attribute�����ݡ�
    /// </summary>
    /// <typeparam name="T">Attribute����</typeparam>
    public static T GetAttr<T> (this Enum e) where T : Attribute
    {
        var memInfo = e.GetType().GetMember(e.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
        return (attributes.Length > 0) ? (T)attributes[0] : null;
    }

    /// <summary>
    /// ��ȡEnum��ÿ��ö��ͷ�ϵ�Description��
    /// </summary>
    public static string GetDescription(this Enum e) {
        var memInfo = e.GetType().GetMember(e.ToString());
        var description = memInfo[0].GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;

        if (description != null) {
            return description.Description;
        }

        return "No description";
    }
}
