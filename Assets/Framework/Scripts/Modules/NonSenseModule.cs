using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 放一个Mono模块测试用，没有功能。
/// </summary>
public class NonSenseModule : MonoBehaviour, IGameModule
{
    public void Init()
    {
        DevUtils.Log("Inited", "NonSenseModule");
    }
}
