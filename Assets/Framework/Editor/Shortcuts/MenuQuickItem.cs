using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ��ӵ����˵�Framework/QuickItems��Ŀ�ݲ�����
/// </summary>
public class MenuQuickItem
{
    /// <summary>
    /// ����Ŀ��ϵͳ�е�����Ŀ¼����Ҫ���ڴ浵�ȣ���
    /// </summary>
    [MenuItem("Framework/QuickItems/Open PersistentDataPath")]
    private static void OpenPersistentDataPath() {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
