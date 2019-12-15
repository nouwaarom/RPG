/// <summary>
/// Vital bar.cs
/// Elbert van de Put
/// 17 okt, 2012
/// 
/// This script is responsible for displaying a vital of out targeted mob.
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour 
{
	public bool _isPlayerHealthBar;	//tells if this is playervitalbar or mob vitalhbar
	private int _maxBarLength;			//this is the max length of the vitalbar
	private int _curBarLength;			//this is the current length of the vitalbar
	private GUITexture _display;
	
	void Awake()
	{
		_display = gameObject.GetComponent<GUITexture>();
	}
	
	// use this for initialization
	void Start () 
	{
		_maxBarLength = (int)_display.pixelInset.width;
		
		OnEnable();
	}
	
	//called once per frame
	void Update () {
	
	}
	
	//called when gameobject is enabled
	public void OnEnable()
	{
		if(_isPlayerHealthBar)
		{
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		}
		else
		{
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	//called when gameobject is disabled
	public void OnDisable()
	{
		if(_isPlayerHealthBar)
		{
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		}
		else
		{
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	
	//calculate the size of the healthbar according to the percentage of vital the target has left
	public void OnChangeHealthBarSize(int curHealth, int maxHealth)
	{
		_curBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength); 		//calculates the current vitalbar length
		_display.pixelInset = CalculatePosition();
	}
	
	//setting the healthbar to the player or mob
	public void SetPlayerHealth(bool b)
	{
		_isPlayerHealthBar = b;
	}
	
	
	private Rect CalculatePosition()
	{
		float yPos = _display.pixelInset.y / 2 - 10;
		
		if(!_isPlayerHealthBar)
		{
			float xPos = (_maxBarLength - _curBarLength) - (_maxBarLength / 4 + 10);
			return new Rect( xPos, yPos, _curBarLength, _display.pixelInset.height);
		}
		
		return new Rect(_display.pixelInset.x, yPos, _curBarLength, _display.pixelInset.height);
	}
	
	private void ToggleDisplay(bool show)
	{
		Debug.Log("Change Mob Vital Bar Display " + show);
		_display.enabled = show;
	}
}
