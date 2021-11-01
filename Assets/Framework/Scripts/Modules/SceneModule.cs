using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ģ�顣
/// ���Ƴ����ļ��غͼ��
/// </summary>
public class SceneModule : IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.Register<SceneModule>();
        DevUtils.Log("Inited", "SceneModule");
    }
}
