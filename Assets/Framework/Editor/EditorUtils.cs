using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// 面向Editor开发时的小工具。
/// </summary>
public static class EditorUtils
{
    /// <summary>
    /// 生成新Prefab的路径。
    /// 这些Prefab往往要在运行时加载，所以统一放在Resources下，且不暴露给用户修改。
    /// </summary>
    static string PREFAB_TARGET_PATH = "Assets/Game/Resources/Prefabs/";

    public enum NewPrefabMode { 
        OriginalAsBase, //生成原Prefab的Variant。
        BrandNew //生成全新的Prefab。
    }

    /// <summary>
    /// 以prefab为模板生成新的Prefab。
    /// </summary>
    /// <param name="prefab">从项目指定的模板Prefab中选择</param>
    /// <returns></returns>
    public static GameObject CreateFromPrefab(Internal_PrefabEnum prefab, NewPrefabMode mode) {
        string prefabPath = prefab.GetDescription();
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        var inited = (GameObject)PrefabUtility.InstantiatePrefab(go);

        if (!Directory.Exists(PREFAB_TARGET_PATH + go.name)) {
            Directory.CreateDirectory(PREFAB_TARGET_PATH + go.name);
        }
        int index = 0;
        while (File.Exists(ConstructPrefabPath(go.name, index)))
        {
            index++;
        }
        if (mode == NewPrefabMode.BrandNew) PrefabUtility.UnpackPrefabInstance(inited, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
        GameObject newPrefab = PrefabUtility.SaveAsPrefabAsset(inited, ConstructPrefabPath(go.name, index), out bool success);
        DevUtils.Log(success);
        if (Application.isPlaying) Object.Destroy(inited);
        else Object.DestroyImmediate(inited);
        return newPrefab;
    }

    /// <summary>
    /// 新Prefab的存放路径。
    /// 例如：由PopupWindow生成的Prefab，依次会是Resources/Prefabs/PopupWindow/PopupWindow_0、
    /// Resources/Prefabs/PopupWindow/PopupWindow_1等。
    /// </summary>
    private static string ConstructPrefabPath(string name, int index) => PREFAB_TARGET_PATH + name + "/" + name + "_" + index.ToString() + ".prefab";


    /// <summary>
    /// 反过来理解就是备份恢复的结果了，通用一下。
    /// </summary>
    public enum BackUpResult 
    { 
        OriginDontExist,
        OriginEmpty,
        CannotWriteTargetFile,
        Success
    }

    /// <summary>
    /// 备份文件，加个bak放在旁边。
    /// </summary>
    /// <param name="path">需要备份的文件路径</param>
    public static BackUpResult BackUpTextFile(string path)
    {
        string backUpPath = path + ".bak";
        if (!File.Exists(path)) return BackUpResult.OriginDontExist;
        string backUp = File.ReadAllText(path);
        if (string.IsNullOrEmpty(backUp)) return BackUpResult.OriginEmpty;
        File.WriteAllText(backUpPath, backUp);
        if (!File.Exists(backUpPath)) return BackUpResult.CannotWriteTargetFile;
        return BackUpResult.Success;
    }

    /// <summary>
    /// （从备份中）恢复文件。
    /// </summary>
    /// <param name="path">被恢复的文件路径（不是备份文件）</param>
    /// <returns></returns>
    public static BackUpResult RestoreTextFile(string path)
    {
        string backUpPath = path + ".bak";
        if (!File.Exists(backUpPath)) return BackUpResult.OriginDontExist;
        string backUp = File.ReadAllText(backUpPath);
        if (string.IsNullOrEmpty(backUp)) return BackUpResult.OriginEmpty;
        File.WriteAllText(path, backUp);
        if (!File.Exists(path)) return BackUpResult.CannotWriteTargetFile;
        return BackUpResult.Success;
    }
}
