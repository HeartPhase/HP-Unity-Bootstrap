using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;
    private void Awake()
    {

    }

    public void TEST1_click()
    {
        cube.GetComponent<MeshRenderer>().material.SetAlpha(0.5f);
    }

    public void TEST2_click()
    {
        DevUtils.Log($"{cube.GetParent()}");
    }
    private void OnDisable()
    {

    }
}
