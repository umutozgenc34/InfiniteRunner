using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform inGameUI;
    [SerializeField] Transform pauseUI;
    [SerializeField] Transform gameoverUI;


    void Start()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper != null)
        {
            scoreKeeper.onScoreChanged += UpdateScoreText;
        }

        GamePlayStatics.GetGameMode().onGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        menuSwitcher.SetActiveUI(gameoverUI);
    }

    private void UpdateScoreText(int newVal)
    {
        scoreText.SetText($"Score : {newVal}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SingnalPause(bool isGamePaused)
    {
        if (isGamePaused)
        {
            menuSwitcher.SetActiveUI(pauseUI);
        }
        else
        {
            menuSwitcher.SetActiveUI(inGameUI);
        }
    }

    public void ResumeGame()
    {
        GamePlayStatics.GetGameMode().SetGamePaused(false);
        menuSwitcher.SetActiveUI(inGameUI);
    }

    public void BackToMenu()
    {
        GamePlayStatics.GetGameMode().BackToMainMenu();
    }

    public void RestartCurrentLevel()
    {
        GamePlayStatics.GetGameMode().RestartCurrentLevel();
    }
}
