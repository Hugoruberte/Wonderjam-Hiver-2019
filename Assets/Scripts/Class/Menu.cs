using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ClickPlayButton()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickHTPButtton()
    {
        SceneManager.LoadScene("HTP");
    }

    public void ClickCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }
}
