using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

/// <summary>
/// ����ʱ��ͨ��С���ߡ�
/// </summary>
public static class DevUtils
{
    /// <summary>
    /// Unity��Debug.Log()���׿ǣ�����һ����΢���㿴�������
    /// ����ָ��'������'���Զ���ȡ���������Log�ķ�����
    /// </summary>
    /// <param name="content">Console�������</param>
    /// <param name="caller">ָ��'������'��Ĭ��Ϊ�����ߵķ�����</param>
    public static void Log(object content, [CallerMemberName] string caller = "Unknown") {
        Debug.Log(string.Format("[{0}] : {1}", caller, content));
    }

    /// <summary>
    /// ֻ��8λ��UUID string���������Ӧ��Ҳ��̫�����ظ��ɡ�
    /// </summary>
    /// <returns>8���ַ�����UUID string</returns>
    public static string SimpleGuid()
    {
        return Guid.NewGuid().ToString().Substring(0,8);
    }
}
