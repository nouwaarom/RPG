/// <summary>
/// Sun.cs
/// Elbert van de Put
/// nov 5, 2012
/// </summary>
using UnityEngine;
using System.Collections;

[AddComponentMenu("Day Night Cycle/Sun")]
public class Sun : MonoBehaviour 
{
	public float maxLightBrightness;
	public float minLightBrightness;
	
	public float maxFlareBrightness;
	public float minFlareBrightness;
	
	public bool GiveLight = false;
	
	void Start()
	{
		if(GetComponent<Light>() != null)
		{
			GiveLight = true;
		}
	}
}
