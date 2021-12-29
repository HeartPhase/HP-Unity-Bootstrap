using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景模块。
/// 控制场景的加载和激活。
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
    /// 显示加载画面，加载名为name的场景。
    /// </summary>
    /// <param name="needConfirm">加载完毕后是否需要玩家确认</param>
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
    /// 异步加载场景并同步加载画面上的进度条。
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
    /// 加载完成后给出按任意键继续的提示。
    /// </summary>
    private void LoadLevelCompleteConfirm() {
        loadingControl.ToggleComplete();
        InputModule.Gameplay_AnyKey += InputModule_Gameplay_AnyKey;
    }

    /// <summary>
    /// 加载完成后无需确认。
    /// </summary>
    private void LoadLevelComplete()
    {
        uiModule.HideUI(loadingScreenPrefab);
    }

    /// <summary>
    /// 任意键后关闭加载画面。
    /// </summary>
    private void InputModule_Gameplay_AnyKey()
    {
        InputModule.Gameplay_AnyKey -= InputModule_Gameplay_AnyKey;
        uiModule.HideUI(loadingScreenPrefab);
    }
}
