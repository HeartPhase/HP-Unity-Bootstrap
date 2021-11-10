using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// ��������ļ���ͬʱҲ��һ�����ص���Ϸ�е�ģ�顣
/// Ϊ����ͬʱ����ⳡ�����塢����inspector�б༭���ɱ�����ģ���ȡ����������ļ�Ҳ��������һ��ģ�顣Ԥ��֮���������������ļ����������������reference��
/// </summary>
[CreateAssetMenu(menuName = "Framework Settings/Framework Settings")]
public class FrameworkSettings : ScriptableObject, IGameModule
{
    [SerializeField]
    public GameObject loadingScreen;

    public void Init() {
        ModuleDispatcher.Instance.Register(this); // ��Ϊ�������ض���ʵ���У�ע��ʱ��Ҫ�ر�ע��thisʵ�������������ᴴ���µĿ����á�
        DevUtils.Log("Settings Load", "FrameworkSettings");
        DevUtils.Log($"{loadingScreen}", "FrameworkSettings");
    }
}
