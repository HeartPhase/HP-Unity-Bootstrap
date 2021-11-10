using UnityEngine;

public class TEST : MonoBehaviour
{
    private void OnEnable()
    {
        //InputModule.Gameplay_MouseScroll += OnMouseScrolledTest;
        //InputModule.PersonPerspective_Camera += OnCameraMovedTest;
    }
    public void TEST1_click()
    {
        ModuleDispatcher.Instance.Get<SceneModule>().LoadLevel("1_SomeLevel", false);
    }

    public void TEST2_click()
    {
        ModuleDispatcher.Instance.Get<InputModule>().ToggleActions("PersonPerspective", false);
    }

    private void OnDisable()
    {
        //InputModule.Gameplay_MouseScroll -= OnMouseScrolledTest;
        //InputModule.PersonPerspective_Camera -= OnCameraMovedTest;
    }

    //private void OnMouseScrolledTest(Vector2 input)
    //{
    //    DevUtils.Log($"Mouse Scrolled {input.y}");
    //}

    //private void OnCameraMovedTest(Vector2 input)
    //{
    //    DevUtils.Log($"Mouse Move {input}");
    //}
}
