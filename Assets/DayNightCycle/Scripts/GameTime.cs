/// <summary>
/// Game time.cs
/// Elbert van de Put
/// nov 5, 2012
/// 
/// this class is responsible for keepint track of the in-game time. And the change of sun and moon and skybox.
/// </summary>
using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour
{
	public enum TimeOfDay
	{
		Idle,
		SunRise,
		SunSet
	}
	public Transform sun;									//an array to hold all suns
	
	public float dayCycleInMinutes = 1;						//how many time a in game day takes
	public float sunRise;
	public float sunSet;
	public float SkyboxBlendModifier;                       //the speed at which the textures will blend in the skybox 

    public Material skyBox;
	public Color ambLightMax;
	public Color ambLightMin;
	
	public float morningLight;
	public float nightLight;
	
	private bool _isMorning = false;
	
	private Sun _sunScript;									//an array to hold the Sun.cs attatched to the main camera
	
	private const float SECOND = 1;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;
	private const float DEGREES_PER_SECOND = 360 / DAY;
	
	private float _degreeRoatation;
	private float _timeOfDay;
	private float _dayCycleInSeconds;
	
	private TimeOfDay _tod;
	private float _noonTime; 								//this is the time of day when it is noon

	// Use this for initialization
	void Start ()
	{
		//disable the sun first because the game starts at midnight
		sun.GetComponent<Light>().enabled = false;
		
		_tod = TimeOfDay.Idle;
		
		//day in seconds = day in minutes * the amount of seconds in a minute
		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;

        //make the skybox blend start at 0 (first skybox)
        RenderSettings.skybox = skyBox;
		RenderSettings.skybox.SetFloat("_Blend", 0);
      
        _sunScript = sun.GetComponent<Sun>();
		
		_timeOfDay = 0;
		_degreeRoatation = DEGREES_PER_SECOND * DAY / (_dayCycleInSeconds);
		
		sunRise *= _dayCycleInSeconds;
		sunSet *= _dayCycleInSeconds;
		morningLight *= _dayCycleInSeconds;
		nightLight *= _dayCycleInSeconds;
		_noonTime = _dayCycleInSeconds / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//set time of day
		_timeOfDay += Time.deltaTime;

        sun.Rotate(new Vector3(_degreeRoatation, 0, 0) * Time.deltaTime);
		
		if(_timeOfDay > _dayCycleInSeconds)
		{
			_timeOfDay -= _dayCycleInSeconds;
		}
		
		//control the outside lightning effects according to the current time
		if(!_isMorning && _timeOfDay > morningLight && _timeOfDay < nightLight)
		{
			_isMorning = true;
			Messenger<bool>.Broadcast("Morning Light Time", true);
            sun.TransformDirection(new Vector3(0, 0, 0)); // Fix the direction at the start of the morning.
			sun.GetComponent<Light>().enabled = true;
			Debug.Log ("Morning");
		}
		
		else if(_isMorning && _timeOfDay > nightLight)
		{
			_isMorning = false;
			Messenger<bool>.Broadcast("Morning Light Time", false);
			sun.GetComponent<Light>().enabled = false;
			Debug.Log("Night");
		}
		
		
		if(_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1)
		{
			_tod = TimeOfDay.SunRise;
			BlendSkybox();
		}
		
		else if(_timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0)
		{
			_tod = TimeOfDay.SunSet;
			BlendSkybox();
		}
		
		else
		{
			_tod = TimeOfDay.Idle;
		}
	}
	
	private void BlendSkybox()
	{
		float temp = 0;
		
		switch(_tod)
		{
			case TimeOfDay.SunRise:
				temp = (_timeOfDay - sunRise) / _dayCycleInSeconds * SkyboxBlendModifier;
				break;
			case TimeOfDay.SunSet:
				temp = (_timeOfDay - sunSet) / _dayCycleInSeconds * SkyboxBlendModifier;
				temp = 1 - temp;
				break;
		}
		
		RenderSettings.skybox.SetFloat("_Blend", temp);
	}	
}
