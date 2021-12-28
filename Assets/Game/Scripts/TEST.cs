using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;

    private void Awake()
    {
        SaveModule.SwitchSaveSlot(SaveModule.GetNextValidSlotNumber());
    }

    public void TEST1_click()
    {
        SaveModule.SaveData(GenTestData(),GenTestData());
    }

    public void TEST2_click()
    {
        SaveModule.WriteSlotSaveFile();
    }

    private string GenTestData()
    {
        return Random.Range(10000000, 99999999).ToString();
    }
    private void OnDisable()
    {

    }
}
