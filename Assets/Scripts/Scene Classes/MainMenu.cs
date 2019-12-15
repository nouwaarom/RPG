/// <summary>
/// Main menu.cs
/// Elbert van de Put
/// nov 7, 2012
/// </summary>
using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	private bool ControlsPressed = false;
	
	public GUISkin MainSkin;
	
	void OnGUI()
	{
		GUI.skin = MainSkin;
		if(ControlsPressed == false)
		{
			displaySingleplayer();
			displayMultiplayer();
			displayControls();
			displayQuit();
		}
		else
		{
			displayControlsMenu();
			displayReturn();
		}
		
	}
	
	private void displaySingleplayer()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 100, 170, 200, 40), "Start Singleplayer"))
		{
			Application.LoadLevel("Character Generator");
		}
	}
	
	private void displayMultiplayer()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 100, 220, 200, 40), "Start Multiplayer"))
		{
			//add multiplayer later
		}
	}
	
	private void displayControls()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 100, 270, 200, 40), "Controls"))
		{
			ControlsPressed = true;
		}
	}
	
	private void displayQuit()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 100, 320, 200, 40), "Quit"))
		{
			Application.Quit();
		}
	}
	
	//voids for the controls menu
	
	private void displayControlsMenu()
	{
		GUI.Label(new Rect(Screen.width / 2 - 100, 150, 200, 40), "wsad to move");
		GUI.Label(new Rect(Screen.width / 2 - 100, 200, 200, 40), "Tab to target mobs");
		GUI.Label(new Rect(Screen.width / 2 - 200, 250, 400, 40), "right mouse button to move camera");
		
	}
	
	private void displayReturn()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 100, 320, 200, 40), "Return"))
		{
			ControlsPressed = false;
		}
	}

}
