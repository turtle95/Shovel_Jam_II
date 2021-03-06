﻿Shader "QuickFur/05-Step/Cutout/Standard" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
		_Glossiness("Smoothness", Range(0,1)) = 0.0
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Length("Length", Range(0.001, 1)) = 0.5
		_Gravity("Gravity", Vector) = (0,-0.2,0,0)
		_WorldGravity("Use World Space Gravity", Range(0,1)) = 0.0
		_Wind("Wind", Vector) = (0.1,0,0.1,0)
		_WindSpeed("Wind Speed", Range(0.1, 10)) = 1
		_WorldWind("Use World Space Wind", Range(0,1)) = 0.0
		_Density("Thickness", Range(0,2)) = 1
		_Brightness("Brightness", Range(0,2)) = 1
		_Occlusion("Fur Occlusion", Range(0,2)) = 0.5
		_MinimumThreshold("Minimum Fur Offset", Range(0,0.1)) = 0.001
	}
		SubShader
		{
					Tags{ "RenderType" = "Opaque" "Queue" = "AlphaTest" }
					LOD 200

					CGINCLUDE
						#pragma target 3.0
						#pragma surface surf Standard fullforwardshadows vertex:vert addshadow alphatest:_Cutoff
			
						#define FUR_STEP 0.2

						#include "../Standard/QuickFur-Standard.cginc"
					ENDCG

					//Run 5 fur passes
					CGPROGRAM
						DOFURPASS(0)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.2)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.4)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.6)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.8)
					ENDCG
	}
	FallBack "Standard"
}
