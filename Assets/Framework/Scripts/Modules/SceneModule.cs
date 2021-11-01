using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景模块。
/// 控制场景的加载和激活。
/// </summary>
public class SceneModule : IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.Register<SceneModule>();
        DevUtils.Log("Inited", "SceneModule");
    }
}
