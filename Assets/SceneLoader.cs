using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                Application.Quit();
            else
                SceneManager.LoadScene("MainMenu");
        }
    }
    public void ToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
