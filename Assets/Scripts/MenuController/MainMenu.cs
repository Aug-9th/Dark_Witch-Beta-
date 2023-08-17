using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start_Game()
    {
        SceneManager.LoadScene("SampleScene");

    }
    public void Exit_Game()
    {
        Debug.Log("Exit Game!");
        Application.Quit();
    }
    
}
