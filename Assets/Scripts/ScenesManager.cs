using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("1Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
