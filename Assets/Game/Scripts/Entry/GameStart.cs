using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameStart
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Start()
    {
        // Maybe I don't need an Entry scene at all.
        // I think for loading the right scene I can possibly handle it in some SceneManagementModule after load the modules.
        LoadModules();
    }

    public static void LoadModules() {
        // todo: load from user config
        ModuleDispatcher.Instance.RegisterMono<NonSenseModule>();
        NonSenseModule nonSenseModule = ModuleDispatcher.Instance.Get<NonSenseModule>();
        DevUtils.Log(nonSenseModule.name);
    }
}
