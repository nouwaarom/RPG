using UnityEngine;
using System.Collections;

public class MainBar : MonoBehaviour
{	
	public static int barLength = 1000; 
	
	private float boxStart = Screen.width / 2 - (barLength / 2);
	private float boxHeight = Screen.height - 60;
	
	private int firstOffset = 5;
	private int secondOffset = 195;
	private int thirdOffset = 385;
	private int fourthOffset = 575;
	private int fifthOffset = 765;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	void OnGUI()
	{
		
		GUI.Box(new Rect(boxStart, boxHeight, barLength, 50), "");
		
		GUI.Button (new Rect(boxStart + firstOffset,		boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + firstOffset + 45,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + firstOffset + 90,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + firstOffset + 135,	boxHeight + 5, 40, 40), "T");
		
		GUI.Button (new Rect(boxStart + secondOffset,		boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + secondOffset + 45,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + secondOffset + 90,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + secondOffset + 135,	boxHeight + 5, 40, 40), "T");
		
		GUI.Button (new Rect(boxStart + thirdOffset,		boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + thirdOffset + 45,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + thirdOffset + 90,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + thirdOffset + 135,	boxHeight + 5, 40, 40), "T");
		
		GUI.Button (new Rect(boxStart + fourthOffset,		boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fourthOffset + 45,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fourthOffset + 90,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fourthOffset + 135,	boxHeight + 5, 40, 40), "T");
		
		GUI.Button (new Rect(boxStart + fifthOffset,		boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fifthOffset + 45,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fifthOffset + 90,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fifthOffset + 135,	boxHeight + 5, 40, 40), "T");
		GUI.Button (new Rect(boxStart + fifthOffset + 180,	boxHeight + 5, 40, 40), "T");
	}
}
