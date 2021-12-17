using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TEST : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] GameObject sphere;

    [SerializeField] TMPro.TMP_Text devText;

    private void Awake()
    {
    }

    public void TEST1_click()
    {
        string testStr = "encrypt me?!noooooooooo";
        devText.text = testStr.Encrypt();
    }

    public void TEST2_click()
    {
        devText.text = devText.text.Decrypt();
    }

    private void OnDisable()
    {
    }
}
