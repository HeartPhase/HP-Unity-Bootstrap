using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��һ��Monoģ������ã�û�й��ܡ�
/// </summary>
public class NonSenseModule : MonoBehaviour, IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.Register<NonSenseModule>();
        DevUtils.Log("Inited", "NonSenseModule");
    }
}
