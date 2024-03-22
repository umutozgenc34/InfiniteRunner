using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform howToPlayMenu;
    [SerializeField] Transform leaderBoardMenu;

    public void Start()
    {
        
        SaveDataManager.SavePlayerProfile("TestPlayer");
    }
    public void StartGame()
    {
        GamePlayStatics.GetGameMode().LoadFirstLevel();
    }

    public void BackToMainMenu()
    {
        menuSwitcher.SetActiveUI(mainMenu);
    }

    public void GoToHowToPlayMenu()
    {
        menuSwitcher.SetActiveUI(howToPlayMenu);
    }

    public void GoToLeaderBoardMenu()
    {
        menuSwitcher.SetActiveUI(leaderBoardMenu);
    }

    public void QuitGame()
    {
        GamePlayStatics.GetGameMode().QuitGame();
    }
}
