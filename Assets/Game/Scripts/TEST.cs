using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    private void Awake()
    {
        InputModule.Gameplay_Interact += LogTestMsg;
    }

    public void TEST1_click()
    {
        ModuleDispatcher.Instance.Get<InputModule>().RemapAction("Gameplay/Interact");
    }

    public void TEST2_click()
    {
    }

    private void LogTestMsg() {
        DevUtils.Log("Interacted", "Test");
    }
    private void OnDisable()
    {
        InputModule.Gameplay_Interact -= LogTestMsg;
    }
}
