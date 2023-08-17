using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] Button pauseBtn;
    public bool isPause = false;
    public GameObject Player;
    private void Start()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        */
    }
    public void Resume()
    {
        //PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    public void Pause()
    {
        //PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
    public void loadMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
    public void quitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
