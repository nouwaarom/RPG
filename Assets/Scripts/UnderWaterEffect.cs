using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/UnderWaterEffect")]
public class UnderWaterEffect : MonoBehaviour
{
	public float underwaterLevel = -2.9f;
	public bool EnableFog = false;
	public float FogDensity = 5.0f;

    void OnGUI()
	{
        if(transform.position.y < underwaterLevel)
		{
       		RenderSettings.fogMode = FogMode.Exponential;
       		RenderSettings.fog = true;
        	RenderSettings.fogDensity = 0.20f;
        	RenderSettings.fogColor = new Color (0f, 0.4f, 0.7f, 0.6f);
       	}

       	else 
		{
          	RenderSettings.fogMode = FogMode.Linear;
       		RenderSettings.fog = EnableFog;
         	RenderSettings.fogDensity = FogDensity;
         	RenderSettings.fogColor = Color.white;

       	}
    }

}