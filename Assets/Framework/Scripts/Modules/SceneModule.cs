using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ģ�顣
/// ���Ƴ����ļ��غͼ��
/// </summary>
public class SceneModule : IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.Register<SceneModule>();
        DevUtils.Log("Inited", "SceneModule");
    }

    public void LoadLevel(string name) {
        //GameObject.Instantiate()
        
    }

    IEnumerator LoadLevelAsync(string name) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone) { 
            float progress = Mathf.Lerp(0f, 1f, operation.progress);
            yield return null;
        }
    }
}
