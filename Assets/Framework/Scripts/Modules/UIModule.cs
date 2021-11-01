using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI模块。控制场景中窗口、界面等的显示。
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{

    public static void Init() {
        ModuleDispatcher.Instance.RegisterMono<UIModule>();
        
        DevUtils.Log("Inited", "UIModule");
    }


}
