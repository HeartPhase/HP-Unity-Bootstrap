using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�������ű���
/// ���ڳ������غ���������ģ�顣
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
    /// ����ģ�顣
    /// ��Ȼ���ǣ������������ģ��ΪʲôҪ���û��Զ��壿д��д����
    /// </summary>
    public static void LoadModules() {
        SceneModule.Init();
        NonSenseModule.Init();
        UIModule.Init();
        InputModule.Init();
    }
}
