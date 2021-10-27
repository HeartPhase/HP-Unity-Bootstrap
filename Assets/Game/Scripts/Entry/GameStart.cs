using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏的启动脚本。
/// 会在场景加载后载入所有模块。
/// </summary>
public static class GameStart
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Start()
    {
        // Maybe I don't need an Entry scene at all.
        // I think for loading the right scene I can possibly handle it in some SceneManagementModule after load the modules.
        LoadModules();
    }

    /// <summary>
    /// 载入模块。
    /// 这里我暂时没有想到实现，待会给自己提一个Issue。
    /// </summary>
    public static void LoadModules() {
        // todo: load from user config
        ModuleDispatcher.Instance.RegisterMono<NonSenseModule>();
        NonSenseModule nonSenseModule = ModuleDispatcher.Instance.Get<NonSenseModule>();
        DevUtils.Log(nonSenseModule.name);
    }
}
