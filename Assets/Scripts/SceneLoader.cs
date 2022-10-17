using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _paused = false;
    [SerializeField] private GameObject _pausePanel; 
    
    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            if (!_paused)
            {
                Time.timeScale = 0;
                _paused = true;
                _pausePanel.SetActive(true);
            } else
            {
                Time.timeScale = 1;
                _paused = false;
                _pausePanel.SetActive(false);
            }
        }
    }
}
