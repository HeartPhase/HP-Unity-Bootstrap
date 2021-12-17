using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏的启动脚本。
/// 会在场景加载后载入所有模块。
/// </summary>
public static class GameStart
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Start()
    {
        LoadSettings();
        LoadModules();
    }
    
    /// <summary>
    /// （从资源文件中）载入配置。
    /// 目前只有一个全局的FrameworkSettings。
    /// </summary>
    public static void LoadSettings() {
        FrameworkSettings settings = FrameworkGlobals.DEFAULT_SETTINGS;
        settings.Init();
    }

    /// <summary>
    /// 载入模块。
    /// 既然都是（几乎）必须的模块为什么要让用户自定义？写死写死。
    /// </summary>
    public static void LoadModules() {
        SceneModule.Init();
        UIModule.Init();
        InputModule.Init();
        EventModule.Init();
    }
}
