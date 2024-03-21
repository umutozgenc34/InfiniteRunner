using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public delegate void OnGameOver();

    [SerializeField] int mainMenuSceneBuildIndex = 0;
    [SerializeField] int firstSceneIndex = 1;

    public event OnGameOver onGameOver;

    bool bIsGamePause = false;
    bool bIsGameOver = false;
   public void GameOver()
    {
        SetGamePaused(true);
        bIsGameOver = true;
        onGameOver?.Invoke();
    }

    internal void LoadFirstLevel()
    {
        LoadScene(firstSceneIndex);
    }

    public void SetGamePaused(bool bIsPaused)
    {
        if (bIsGameOver)
        {
            return;
        }

        bIsGamePause = bIsPaused;
        if (bIsGamePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    internal void TogglePause()
    {
        if (IsGamePaused())
        {
            SetGamePaused(false);
        }
        else
        {
            SetGamePaused(true);
        }
    }
    
    public bool IsGamePaused()
    {
        return bIsGamePause;
    }

    public void BackToMainMenu()
    {
        LoadScene(mainMenuSceneBuildIndex);
    }

    private void LoadScene(int sceneBuildIndex)
    {
        bIsGameOver = false;
        SetGamePaused(false);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    internal void RestartCurrentLevel()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    internal bool IsGameOver()
    {
        return bIsGameOver;
    }
}
