using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void OpenSettings(GameObject popup)
    {
        popup.SetActive(true);
    }
}
