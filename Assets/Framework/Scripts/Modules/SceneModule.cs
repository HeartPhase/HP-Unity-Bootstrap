using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ģ�顣
/// ���Ƴ����ļ��غͼ��
/// </summary>
public class SceneModule : MonoBehaviour, IGameModule
{
    private Coroutine loadingCoroutine;
    private GameObject loadingScreenPrefab;
    private LoadingScreen loadingControl;

    private UIModule uiModule => ModuleDispatcher.Instance.Get<UIModule>();

    public static void Init()
    {
        ModuleDispatcher.Instance.RegisterMono<SceneModule>();
        DevUtils.Log("Inited", "SceneModule");
    }

    /// <summary>
    /// ��ʾ���ػ��棬������Ϊname�ĳ�����
    /// </summary>
    public void LoadLevel(string name) {
        if (loadingScreenPrefab == null) {
            loadingScreenPrefab = ModuleDispatcher.Instance.Get<FrameworkSettings>().loadingScreen;
        }
        loadingControl = uiModule.ShowUI(loadingScreenPrefab).GetComponent<LoadingScreen>();
        loadingCoroutine = StartCoroutine(LoadLevelAsync(name));
    }

    /// <summary>
    /// �첽���س�����ͬ�����ػ����ϵĽ�������
    /// </summary>
    IEnumerator LoadLevelAsync(string name) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone) { 
            float progress = Mathf.Lerp(0f, 1f, operation.progress);
                loadingControl.SetProgress(progress);
            yield return null;
        }
        loadingControl.SetProgress(1.0f);
        LoadLevelCompleteCallback();
    }

    /// <summary>
    /// ������ɺ�������������������ʾ��
    /// </summary>
    private void LoadLevelCompleteCallback() {
        loadingControl.ToggleComplete();
        InputModule.Gameplay_AnyKey += InputModule_Gameplay_AnyKey;
    }

    /// <summary>
    /// �������رռ��ػ��档
    /// </summary>
    private void InputModule_Gameplay_AnyKey()
    {
        InputModule.Gameplay_AnyKey -= InputModule_Gameplay_AnyKey;
        uiModule.HideUI(loadingScreenPrefab);
    }
}
