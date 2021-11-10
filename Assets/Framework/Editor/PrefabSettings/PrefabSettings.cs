using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector;

/// <summary>
/// Prefabģ�����á�
/// ���´���ǳ����������У�α���˹����ܳ�û����������ﾯ�档
/// </summary>
[CreateAssetMenu(menuName ="Framework Settings/Prefab Settings")]
public class PrefabSettings : ScriptableObject
{
    [PropertySpace(16)]
    [HideLabel, DisplayAsString(false)]
    public string info; //������Inspector����ʾ��Ϣ��
    [PropertySpace(16)]


    // һЩĬ��·����һ����˵���Բ��İɡ�
    #region Paths
    [SerializeField] string StandardisedPrefab_Path = "Assets/Framework/StandardisedPrefabs";

    private List<string> prefabPaths = new List<string>();

    [SerializeField] string ENUM_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_PrefabEnum.cs";
    [SerializeField] string CONTEXT_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_ContextMenu.cs";
    #endregion

    // ��ʾ�������ɽű���û�б��ݡ�
    [ReadOnly]
    [SerializeField] bool enum_BackUpReady;

    [ReadOnly]
    [SerializeField] bool context_BackUpReady;

    
    /// <summary>
    /// ����ģ���ļ���һ������ģ��ö�ٺ��Ҽ��˵���
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
    /// ��ȡ�ļ���������Prefab��·����
    /// </summary>
    private void ReadPrefabs() {
        string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {StandardisedPrefab_Path});
        foreach (string guid in guids)
        {
            prefabPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    /// <summary>
    /// ��ģ��Prefab���ƺ�·���Ķ�Ӧ��ϵ����Enum�����֮���ڴ�������á�
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
    /// ����Hierarchy���Լ�GameObject�˵����е�ѡ�����ֱ���Ҽ���ģ�塣
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

    //���ܲ�û��ʲô���ã���д������
    #region BackUp and Restore

    /// <summary>
    /// ����Enum�ļ���
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
    /// �ָ�Enum�ļ���
    /// </summary>
    [EnableIf("enum_BackUpReady")]
    [Button("Restore Enum file")]
    void RestoreEnum()
    {
        EditorUtils.BackUpResult result = EditorUtils.RestoreTextFile(ENUM_FILE);

        info = $"[Restore Result (Enum)] : {result}";
    }

    /// <summary>
    /// ����Context�ű��ļ���
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
    /// �ָ�Context�ļ���
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
