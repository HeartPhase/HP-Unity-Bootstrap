using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public static class DevUtils
{
    public static void Log(object content, [CallerMemberName] string caller = "Unknown") {
        Debug.Log(string.Format("[{0}] : {1}", caller, content));
    }
}
