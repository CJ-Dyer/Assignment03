using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionScript : MonoBehaviour
{
    public void Intro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void PostGame()
    {
        SceneManager.LoadScene("PostGameScene");
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
