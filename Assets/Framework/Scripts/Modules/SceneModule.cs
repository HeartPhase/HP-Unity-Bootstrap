using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ģ�顣
/// ���Ƴ����ļ��غͼ��
/// </summary>
public class SceneModule : MonoBehaviour, IGameModule
{
    public static void Init()
    {
        ModuleDispatcher.Instance.RegisterMono<SceneModule>();
        DevUtils.Log("Inited", "SceneModule");
    }

    private Coroutine loadingCoroutine;
    private GameObject loadingScreenPrefab;
    private LoadingScreen loadingControl;

    private UIModule uiModule => ModuleDispatcher.Instance.Get<UIModule>();


    /// <summary>
    /// ��ʾ���ػ��棬������Ϊname�ĳ�����
    /// </summary>
    /// <param name="needConfirm">������Ϻ��Ƿ���Ҫ���ȷ��</param>
    public void LoadLevel(string name, bool needConfirm = true) {
        if (loadingScreenPrefab == null)
        {
            loadingScreenPrefab = ModuleDispatcher.Instance.Get<FrameworkSettings>().loadingScreen;
        }
        loadingControl = uiModule.ShowUI(loadingScreenPrefab).GetComponent<LoadingScreen>();
        if (needConfirm) {
            loadingCoroutine = StartCoroutine(LoadLevelAsync(name, LoadLevelCompleteConfirm)); 
        }
        else {
            loadingCoroutine = StartCoroutine(LoadLevelAsync(name, LoadLevelComplete));
        }
        
    }

    /// <summary>
    /// �첽���س�����ͬ�����ػ����ϵĽ�������
    /// </summary>
    IEnumerator LoadLevelAsync(string name, Action onLoad) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone) { 
            float progress = Mathf.Lerp(0f, 1f, operation.progress);
                loadingControl.SetProgress(progress);
            yield return null;
        }
        loadingControl.SetProgress(1.0f);
        onLoad();
    }

    /// <summary>
    /// ������ɺ�������������������ʾ��
    /// </summary>
    private void LoadLevelCompleteConfirm() {
        loadingControl.ToggleComplete();
        InputModule.Gameplay_AnyKey += InputModule_Gameplay_AnyKey;
    }

    /// <summary>
    /// ������ɺ�����ȷ�ϡ�
    /// </summary>
    private void LoadLevelComplete()
    {
        uiModule.HideUI(loadingScreenPrefab);
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
