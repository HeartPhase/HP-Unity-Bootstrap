using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// ��������ļ���ͬʱҲ��һ�����ص���Ϸ�е�ģ�顣
/// �������ļ�������޸���λ����Ҫ�޸�Framework Globals�ж�Ӧ·����
/// Ϊ����ͬʱ����ⳡ�����塢����inspector�б༭���ɱ�����ģ���ȡ����������ļ�Ҳ��������һ��ģ�顣Ԥ��֮���������������ļ����������������reference��
/// </summary>
public class FrameworkSettings : ScriptableObject, IGameModule
{
    [SerializeField]
    public GameObject loadingScreen;

    [SerializeField]
    public UISettings uiSettings;

    public void Init() {
        ModuleDispatcher.Instance.Register(this); // ��Ϊ�������ض���ʵ���У�ע��ʱ��Ҫ�ر�ע��thisʵ�������������ᴴ���µĿ����á�
        DevUtils.Log("Settings Load", "FrameworkSettings");
    }
}
