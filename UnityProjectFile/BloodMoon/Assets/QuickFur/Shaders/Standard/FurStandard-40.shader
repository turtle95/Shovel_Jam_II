Shader "QuickFur/40-Step/Standard" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
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
					Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
					LOD 200

					ZWrite Off
					Blend SrcAlpha OneMinusSrcAlpha

					CGINCLUDE
						#pragma target 3.0
						#pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
			
						#define FUR_STEP 0.025
						#define FUR_LAYERS 40

						#include "QuickFur-Standard.cginc"
					ENDCG

					//Run 40 fur passes
					CGPROGRAM
						DOFURPASS(0)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.025)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.05)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.075)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.1)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.125)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.15)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.175)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.2)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.225)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.25)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.275)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.3)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.325)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.35)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.375)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.4)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.425)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.45)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.475)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.5)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.525)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.55)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.575)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.6)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.625)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.65)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.675)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.7)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.725)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.75)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.775)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.8)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.825)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.85)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.875)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.9)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.925)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.95)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.975)
					ENDCG
	}
	FallBack "Standard"
}
