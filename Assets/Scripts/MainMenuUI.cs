using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] UISwitcher menuSwitcher;
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform howToPlayMenu;
    [SerializeField] Transform leaderBoardMenu;
    [SerializeField] Transform createPlayerProfileMenu;
    [SerializeField] TMP_InputField newPlayerNameField;

    public void Start()
    {
        
        
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

    public void SwitchToPlayerProfileMenu()
    {
        menuSwitcher.SetActiveUI(createPlayerProfileMenu);
    }

    public void AddPlayerProfile()
    {
        string newPlayerName = newPlayerNameField.text;
    }
}
