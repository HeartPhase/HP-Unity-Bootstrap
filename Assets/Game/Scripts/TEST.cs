using UnityEngine;

public class TEST : MonoBehaviour
{
    EventModule eventModule;
    private void Awake()
    {
        //eventModule = ModuleDispatcher.Instance.Get<EventModule>();
        InputModule.Gameplay_MouseScroll += OnMouseScrolledTest;
        InputModule.PersonPerspective_Camera += OnCameraMovedTest;
    }
    public void TEST1_click()
    {
        ModuleDispatcher.Instance.Unregister<UIModule>();
    }

    public void TEST2_click()
    {
        
    }

    private void TESTAction(EventArgs args) {
        DevUtils.Log($"{args.ToString()}");
    }

    private void OnDisable()
    {
        InputModule.Gameplay_MouseScroll -= OnMouseScrolledTest;
        InputModule.PersonPerspective_Camera -= OnCameraMovedTest;
    }

    private void OnMouseScrolledTest(Vector2 input)
    {
        DevUtils.Log($"Mouse Scrolled {input.y}");
    }

    private void OnCameraMovedTest(Vector2 input)
    {
        DevUtils.Log($"Mouse Move {input}");
    }
}
