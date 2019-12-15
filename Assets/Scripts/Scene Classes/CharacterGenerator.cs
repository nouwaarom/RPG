/// <summary>
/// Character generator.cs
/// Elbert van de Put
/// nov 2, 2012
/// </summary>
using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour 
{
	private PlayerCharacter _toon;
	
	private const int STARTING_POINTS = 10;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 5;
	
	private int pointsleft;
	
	public GUISkin CharacterGeneratorSkin;
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () 
	{
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "Player Character";
		
		//disable the control of the playercharacter
		pc.GetComponent<Movement>().enabled = false;
		
		_toon = pc.GetComponent<PlayerCharacter>();
		
		pointsleft = STARTING_POINTS;
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
			_toon.GetPrimaryAttribute(cnt).BaseValue = MIN_STARTING_ATTRIBUTE_VALUE;
		_toon.StatUpdate();
	}
	
	void OnGUI()
	{
		GUI.skin = CharacterGeneratorSkin;
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
		
		//display button if name is filled and points are spent otherwise show a unclickable label
		if(_toon.Name == "" || pointsleft > 0)
			DisplayCreateLabel();
		else
			DisplayCreateButton();
		
		DisplayReturnButton();
	}
	
	private void DisplayName()
	{
		GUI.Label(new Rect(20,			//x
						   20,			//z
						   70, 			//width
						   35),			//height
						   "Name: ");	//display:
		
		_toon.Name = GUI.TextField(new Rect(110,
											20,
											180, 
											35), _toon.Name);
	}
	
	private void DisplayPointsLeft()
	{
		GUI.Label(new Rect(1000, 
							20, 
							150,
							35),
							"Points left: " + pointsleft);
	}
	
	private void DisplayAttributes()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			GUI.Label(new Rect(20,
								55 + ((cnt + 1) * 30),
								100 , 35),
								((AttributeName)cnt).ToString());
			
			GUI.Label(new Rect(155,
								55 + ((cnt + 1) * 30),
								30 ,
								35), _toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			
			if(GUI.Button(new Rect(190,
									55 + ((cnt + 1) * 30),
									30,
									30),
									"-"))
			{
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE)
				{
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsleft++;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button(new Rect(225, 
									55 + ((cnt + 1) * 30),
									30,
									30),
									"+"))
			{
				if(pointsleft > 0)
				{
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsleft--;
					_toon.StatUpdate();
				}
			}
		}
	}
	
	private void DisplayVitals()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			GUI.Label(new Rect(20, 
								55 + ((cnt + 7) * 30),
								100,
								35),
								((VitalName)cnt).ToString());
			
			GUI.Label(new Rect(155, 
								55 + ((cnt + 7) * 30),
								30,
								35),
								_toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}
	
	private void DisplaySkills()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
		{
			GUI.Label(new Rect(1000, 
								55 + ((cnt + 1) * 30),
								100,
								35),
								((SkillName)cnt).ToString().Replace("_", " "));
			
			GUI.Label(new Rect(1150,
								55 + ((cnt + 1)* 30),
								30,
								35),
								_toon.GetSkill(cnt).AdjustedBaseValue.ToString());
			
		}
	}
	
	private void DisplayCreateLabel()
	{
		GUI.Label(new Rect(550,
							Screen.height - 100,
							120,
							35), 
							"Creating...", "Button");
	}
	
	private void DisplayCreateButton()
	{
		if(GUI.Button(new Rect(550,
						    	Screen.height - 100,
								120,
							    35), 
								"Create"))
		{	
			GameSettings gsScript = GameObject.Find("*Game Settings").GetComponent<GameSettings>();
			
			//change curvalue of the vitals to the max modified value of that vital
			UpdateCurVitalValues();
			
			//save character data
			gsScript.SaveCharacterData();
			
			Application.LoadLevel("World1");
		}
	}
	
	private void DisplayReturnButton()
	{
		if(GUI.Button(new Rect(550,
						    	Screen.height - 50,
								220,
							    35), 
								"return to mainmenu"))
		{	
			//make sure to delete unwanted objects
			Destroy(GameObject.Find("*Game Settings"));
			Application.LoadLevel("Main Menu");
		}
	}
	
	private void UpdateCurVitalValues()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}
}