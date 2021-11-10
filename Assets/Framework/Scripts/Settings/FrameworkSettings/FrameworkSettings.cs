using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// 框架配置文件，同时也是一个加载到游戏中的模块。
/// 为了能同时达成免场景物体、可在inspector中编辑、可被其他模块读取，这个配置文件也被做成了一个模块。预计之后其他的子配置文件都会用这个配置来reference。
/// </summary>
[CreateAssetMenu(menuName = "Framework Settings/Framework Settings")]
public class FrameworkSettings : ScriptableObject, IGameModule
{
    [SerializeField]
    public GameObject loadingScreen;

    public void Init() {
        ModuleDispatcher.Instance.Register(this); // 因为配置在特定的实例中，注册时需要特别注册this实例。其他方法会创建新的空配置。
        DevUtils.Log("Settings Load", "FrameworkSettings");
        DevUtils.Log($"{loadingScreen}", "FrameworkSettings");
    }
}
