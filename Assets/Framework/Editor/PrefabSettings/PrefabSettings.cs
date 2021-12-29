using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector;

/// <summary>
/// Prefab模板配置。
/// 以下代码非常暴力，还有（伪）人工智能出没，光敏性癫痫警告。
/// </summary>
[CreateAssetMenu(menuName ="Framework Settings/Prefab Settings")]
public class PrefabSettings : ScriptableObject
{
    [PropertySpace(16)]
    [HideLabel, DisplayAsString(false)]
    public string info; //用来在Inspector里显示信息。
    [PropertySpace(16)]


    // 一些默认路径，一般来说可以不改吧。
    #region Paths
    [SerializeField] string StandardisedPrefab_Path = "Assets/Framework/StandardisedPrefabs";

    private List<string> prefabPaths = new List<string>();

    [SerializeField] string ENUM_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_PrefabEnum.cs";
    [SerializeField] string CONTEXT_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_ContextMenu.cs";
    #endregion

    // 显示两个生成脚本有没有备份。
    [ReadOnly]
    [SerializeField] bool enum_BackUpReady;

    [ReadOnly]
    [SerializeField] bool context_BackUpReady;

    
    /// <summary>
    /// 根据模板文件夹一键生成模板枚举和右键菜单。
    /// </summary>
    [Button("Set Up")]
    void SetUp() {
        if (string.IsNullOrEmpty(StandardisedPrefab_Path)) return;
        prefabPaths.Clear();
        ReadPrefabs();
        WritePrefabsEnumFile();
        WriteContextMenuFile();
    }

    /// <summary>
    /// 获取文件夹中所有Prefab的路径。
    /// </summary>
    private void ReadPrefabs() {
        string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {StandardisedPrefab_Path});
        foreach (string guid in guids)
        {
            prefabPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    /// <summary>
    /// 将模板Prefab名称和路径的对应关系存在Enum里，方便之后在代码里调用。
    /// </summary>
    void WritePrefabsEnumFile() {
        if (prefabPaths.Count == 0) ReadPrefabs();
        string enumfile = "";
        enumfile += FrameworkGlobals.GENERATED_SCRIPTS_WARNING;
        string enumPrefix = "using System.ComponentModel; \npublic enum Internal_PrefabEnum { \n";
        string enumSuffix = "}\n";

        enumfile += enumPrefix;

        BackUpEnum();
        foreach (string path in prefabPaths) {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            DevUtils.Log(path);
            DevUtils.Log(prefab.name);
            enumfile += string.Format("    [Description(\"{0}\")]\n", path);
            enumfile += string.Format("    {0},\n", prefab.name);
        }
        enumfile += enumSuffix;
        File.WriteAllText(ENUM_FILE, enumfile);
    }

    /// <summary>
    /// 生成Hierarchy（以及GameObject菜单）中的选项，方便直接右键套模板。
    /// </summary>
    void WriteContextMenuFile()
    {
        if (prefabPaths.Count == 0) ReadPrefabs();
        string contextFile = "";
        contextFile += FrameworkGlobals.GENERATED_SCRIPTS_WARNING;
        string contextPrefix = "using UnityEditor;\npublic class Internal_ContextMenu {\n";
        string contextSuffix = "}\n";

        contextFile += contextPrefix;

        BackUpContext();
        foreach (string path in prefabPaths) 
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            contextFile += $"[MenuItem(\"GameObject/Templates/{prefab.name}\")]\n";
            contextFile += $"static void Create{prefab.name}() {{\n";
            contextFile += $"PrefabUtility.InstantiatePrefab(EditorUtils.CreateFromPrefab(Internal_PrefabEnum.{prefab.name}, EditorUtils.NewPrefabMode.BrandNew));}}\n";
        }

        contextFile += contextSuffix;
        File.WriteAllText(CONTEXT_FILE, contextFile);
    }

    //可能并没有什么卵用，先写在这里
    #region BackUp and Restore

    /// <summary>
    /// 备份Enum文件。
    /// </summary>
    [Button("Back up Enum file")]
    void BackUpEnum()
    {
        EditorUtils.BackUpResult result = EditorUtils.BackUpTextFile(ENUM_FILE);

        info = $"[Back Up Result (Enum)] : {result}";
        if (result == EditorUtils.BackUpResult.Success)
        {
            enum_BackUpReady = true;
        }
    }

    /// <summary>
    /// 恢复Enum文件。
    /// </summary>
    [EnableIf("enum_BackUpReady")]
    [Button("Restore Enum file")]
    void RestoreEnum()
    {
        EditorUtils.BackUpResult result = EditorUtils.RestoreTextFile(ENUM_FILE);

        info = $"[Restore Result (Enum)] : {result}";
    }

    /// <summary>
    /// 备份Context脚本文件。
    /// </summary>
    [Button("Back up Context file")]
    void BackUpContext()
    {
        EditorUtils.BackUpResult result = EditorUtils.BackUpTextFile(CONTEXT_FILE);

        info = $"[Back Up Result (Context)] : {result}";
        if (result == EditorUtils.BackUpResult.Success)
        {
            context_BackUpReady = true;
        }
    }

    /// <summary>
    /// 恢复Context文件。
    /// </summary>
    [EnableIf("context_BackUpReady")]
    [Button("Restore Enum file")]
    void RestoreContext()
    {
        EditorUtils.BackUpResult result = EditorUtils.RestoreTextFile(CONTEXT_FILE);

        info = $"[Restore Result (Context)] : {result}";
    }

    #endregion
}
