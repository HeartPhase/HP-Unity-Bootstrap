using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�������ű���
/// ���ڳ������غ���������ģ�顣
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
    /// ������Դ�ļ��У��������á�
    /// Ŀǰֻ��һ��ȫ�ֵ�FrameworkSettings��
    /// </summary>
    public static void LoadSettings() {
        FrameworkSettings settings = FrameworkGlobals.DEFAULT_SETTINGS;
        settings.Init();
    }

    /// <summary>
    /// ����ģ�顣
    /// ��Ȼ���ǣ������������ģ��ΪʲôҪ���û��Զ��壿д��д����
    /// </summary>
    public static void LoadModules() {
        SceneModule.Init();
        UIModule.Init();
        InputModule.Init();
        EventModule.Init();
    }
}
