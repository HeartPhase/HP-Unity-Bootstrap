using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

/// <summary>
/// 开发时的通用小工具。
/// </summary>
public static class DevUtils
{
    /// <summary>
    /// Unity中Debug.Log()的套壳，换成一个稍微方便看的输出。
    /// 可以指定'发言者'或自动获取调用了这个Log的方法。
    /// </summary>
    /// <param name="content">Console输出内容</param>
    /// <param name="caller">指定'发言者'，默认为调用者的方法名</param>
    public static void Log(object content, [CallerMemberName] string caller = "Unknown") {
        Debug.Log(string.Format("[{0}] : {1}", caller, content));
    }
}
