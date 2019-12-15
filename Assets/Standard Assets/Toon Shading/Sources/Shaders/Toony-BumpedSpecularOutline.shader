Shader "Toon/BumpedSpecularOutline" 
{
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.03, 1)) = 0.078125
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_BumpMap ("Normalmap", 2D) = "bump" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {} 
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		
		
		UsePass "Toon/Lighted/FORWARD"
		UsePass "Toon/Basic Outline/OUTLINE"
		LOD 400
		
	CGPROGRAM
	#pragma surface surf BlinnPhong
	
	
	sampler2D _MainTex;
	sampler2D _BumpMap;
	fixed4 _Color;
	half _Shininess;
	
	struct Input 
	{
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};
	
	void surf (Input IN, inout SurfaceOutput o) 
	{
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color.rgb;
		o.Gloss = tex.a;
		o.Alpha = tex.a * _Color.a;
		o.Specular = _Shininess;
		o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
	}
	ENDCG
	} 
	
	Fallback "Specular"
}