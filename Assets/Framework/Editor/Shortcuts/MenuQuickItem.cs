using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 添加到主菜单Framework/QuickItems里的快捷操作。
/// </summary>
public class MenuQuickItem
{
    /// <summary>
    /// 打开项目在系统中的数据目录（主要用于存档等）。
    /// </summary>
    [MenuItem("Framework/QuickItems/Open PersistentDataPath")]
    private static void OpenPersistentDataPath() {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
