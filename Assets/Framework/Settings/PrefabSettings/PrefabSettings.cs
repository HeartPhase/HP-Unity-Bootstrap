using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(menuName ="Framework Settings/Prefab Settings")]
public class PrefabSettings : ScriptableObject
{
    //static string MODULE_NAME = "Standardised Prefab Collector";

    [SerializeField] string StandardisedPrefab_Path = "Assets/Framework/StandardisedPrefabs";

    private List<string> prefabPaths = new List<string>();

    [SerializeField] string ENUM_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_PrefabEnum.cs";
    [SerializeField] string CONTEXT_FILE = "Assets/Framework/Settings/PrefabSettings/Internal_ContextMenu.cs";

    // todo: change this to a button, I don't have odin at work env.
    //private void OnValidate()
    //{
    //    if (string.IsNullOrEmpty(StandardisedPrefab_Path)) return;
    //    prefabPaths.Clear();
    //    ReadPrefabs();
    //    WritePrefabsEnumFile();
    //}

    [ContextMenu("Do SetUp")]
    void SetUp() {
        if (string.IsNullOrEmpty(StandardisedPrefab_Path)) return;
        prefabPaths.Clear();
        ReadPrefabs();
        WritePrefabsEnumFile();
        WriteContextMenuFile();
    }

    private void ReadPrefabs() {
        string[] guids = AssetDatabase.FindAssets("t:prefab", new[] {StandardisedPrefab_Path});
        foreach (string guid in guids)
        {
            prefabPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
    }

    [ContextMenu("Generate/Enum")]
    void WritePrefabsEnumFile() {
        if (prefabPaths.Count == 0) ReadPrefabs();
        string enumfile = "";
        string enumPrefix = "using System.ComponentModel; \npublic enum Internal_PrefabEnum { \n";
        string enumSuffix = "}\n";

        enumfile += enumPrefix;

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

    [ContextMenu("Generate/Context Menu")]
    void WriteContextMenuFile()
    {
        if (prefabPaths.Count == 0) ReadPrefabs();
        string contextFile = "";
        string contextPrefix = "using UnityEditor;\npublic class Internal_ContextMenu {\n";
        string contextSuffix = "}\n";

        contextFile += contextPrefix;

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
}
