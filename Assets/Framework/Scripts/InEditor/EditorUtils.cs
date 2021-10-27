using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// ����Editor����ʱ��С���ߡ�
/// </summary>
public static class EditorUtils
{
    /// <summary>
    /// ������Prefab��·����
    /// ��ЩPrefab����Ҫ������ʱ���أ�����ͳһ����Resources�£��Ҳ���¶���û��޸ġ�
    /// </summary>
    static string PREFAB_TARGET_PATH = "Assets/Game/Resources/Prefabs/";

    public enum NewPrefabMode { 
        OriginalAsBase, //����ԭPrefab��Variant��
        BrandNew //����ȫ�µ�Prefab��
    }

    /// <summary>
    /// ��prefabΪģ�������µ�Prefab��
    /// </summary>
    /// <param name="prefab">����Ŀָ����ģ��Prefab��ѡ��</param>
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
    /// ��Prefab�Ĵ��·����
    /// ���磺��PopupWindow���ɵ�Prefab�����λ���Resources/Prefabs/PopupWindow/PopupWindow_0��
    /// Resources/Prefabs/PopupWindow/PopupWindow_1�ȡ�
    /// </summary>
    private static string ConstructPrefabPath(string name, int index) => PREFAB_TARGET_PATH + name + "/" + name + "_" + index.ToString() + ".prefab";


    /// <summary>
    /// �����������Ǳ��ݻָ��Ľ���ˣ�ͨ��һ�¡�
    /// </summary>
    public enum BackUpResult 
    { 
        OriginDontExist,
        OriginEmpty,
        CannotWriteTargetFile,
        Success
    }

    /// <summary>
    /// �����ļ����Ӹ�bak�����Աߡ�
    /// </summary>
    /// <param name="path">��Ҫ���ݵ��ļ�·��</param>
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
    /// ���ӱ����У��ָ��ļ���
    /// </summary>
    /// <param name="path">���ָ����ļ�·�������Ǳ����ļ���</param>
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
