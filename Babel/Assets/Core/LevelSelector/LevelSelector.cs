﻿using UnityEngine;
using System.Collections;

public class LevelSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

	public void LoadLevel(string SceneName)
	{
		if(!SceneName.Equals(""))
		{
			Application.LoadLevel (SceneName);
		}
	}

	public void BackToMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
