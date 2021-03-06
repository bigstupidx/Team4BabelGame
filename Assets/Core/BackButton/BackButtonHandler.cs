﻿using UnityEngine;
using System.Collections;
using System;
using Assets.Core.Configuration;

public class BackButtonHandler : MonoBehaviour {

    private String currentScene;
    public String mainScene;
    public String levelSelectScene;
    public String LoadingScreen1;
    public String LoadingScreen2;
    public String LoadingScreen3;


    // Use this for initialization
    void Start () {
        currentScene = Application.loadedLevelName;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene.Equals(LoadingScreen1) || currentScene.Equals(LoadingScreen2) || currentScene.Equals(LoadingScreen3)) ;
            else if (currentScene.Equals(mainScene)) mainMenuActions();
            else if (currentScene.Equals("BackButtonTest")) Application.Quit();
            else if (currentScene.Equals(levelSelectScene)) Application.LoadLevel(mainScene);
            else defaultAction();
        }
    }

    private void defaultAction()
    {
        //Application.LoadLevel(mainScene);
        if (Time.timeScale > 0.9f)
        {
            GameObject.FindObjectOfType<PauseScreen>().PausePanelBtn();
        }
        else
        {
            if(FindObjectOfType<PauseScreen>() != null)
                FindObjectOfType<PauseScreen>().PausePanelBackBtn();
        }
        
    }

    private void mainMenuActions()
    {

        // GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().ActivateDialogWindow("Exit Game", "Do you want to exit the game?", true);

        if (GameObject.FindObjectOfType<MainMenuManager>().SettingsPanel.active)
            GameObject.FindObjectOfType<MainMenuManager>().SettingsBackBtnPress();
        else if (GameObject.FindObjectOfType<MainMenuManager>().CreditsPanel.active)
            GameObject.FindObjectOfType<MainMenuManager>().CreditsBackBtnPress();
        else
            GameObject.FindGameObjectWithTag(Constants.Tags.WindowManager).GetComponent<WindowHandler>().CreateConfirmDialog("Phrases/ExitGame", "Phrases/DoYouWantToExit", "Phrases/YesText", "Phrases/NoText", QuitGame, null);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
