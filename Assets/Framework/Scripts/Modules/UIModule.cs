using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIģ�顣���Ƴ����д��ڡ�����ȵ���ʾ��
/// </summary>
public class UIModule : MonoBehaviour, IGameModule
{

    public static void Init() {
        ModuleDispatcher.Instance.RegisterMono<UIModule>();
        
        DevUtils.Log("Inited", "UIModule");
    }


}
