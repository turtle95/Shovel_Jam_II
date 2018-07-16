Shader "QuickFur/40-Step/Toon Basic" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_ToonShade("ToonShader Cubemap(RGB)", CUBE) = "" {}
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
						#pragma vertex vert
						#pragma fragment frag 
						#pragma multi_compile_fog
			
						#define FUR_STEP 0.025
						#define FUR_LAYERS 40

						#include "QuickFur-ToonBasic.cginc"
					ENDCG

					//Run 40 fur passes
					Pass 
					{ 
						Name "Fur 0"
						CGPROGRAM
							DOFURPASS(0)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.025"
						CGPROGRAM
							DOFURPASS(0.025)
						ENDCG
					}
					Pass
					{
						Name "Fur 0.05"
						CGPROGRAM
							DOFURPASS(0.05)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.075"
						CGPROGRAM
							DOFURPASS(0.075)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.1"
						CGPROGRAM
							DOFURPASS(0.1)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.125"
						CGPROGRAM
							DOFURPASS(0.125)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.15"
						CGPROGRAM
							DOFURPASS(0.15)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.175"
						CGPROGRAM
							DOFURPASS(0.175)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.2"
						CGPROGRAM
							DOFURPASS(0.2)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.225"
						CGPROGRAM
							DOFURPASS(0.225)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.25"
						CGPROGRAM
							DOFURPASS(0.25)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.275"
						CGPROGRAM
							DOFURPASS(0.275)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.3"
						CGPROGRAM
							DOFURPASS(0.3)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.325"
						CGPROGRAM
							DOFURPASS(0.325)
						ENDCG
					}
					Pass
					{
						Name "Fur 0.35"
						CGPROGRAM
							DOFURPASS(0.35)
						ENDCG
					}
					Pass
					{
						Name "Fur 0.375"
						CGPROGRAM
							DOFURPASS(0.375)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.4"
						CGPROGRAM
							DOFURPASS(0.4)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.425"
						CGPROGRAM
							DOFURPASS(0.425)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.45"
						CGPROGRAM
							DOFURPASS(0.45)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.475"
						CGPROGRAM
							DOFURPASS(0.475)
						ENDCG
					}
					Pass
					{
						Name "Fur 0.5"
						CGPROGRAM
							DOFURPASS(0.5)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.525"
						CGPROGRAM
							DOFURPASS(0.525)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.55"
						CGPROGRAM
							DOFURPASS(0.55)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.575"
						CGPROGRAM
							DOFURPASS(0.575)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.6"
						CGPROGRAM
							DOFURPASS(0.6)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.625"
						CGPROGRAM
							DOFURPASS(0.625)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.65"
						CGPROGRAM
							DOFURPASS(0.65)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.675"
						CGPROGRAM
							DOFURPASS(0.675)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.7"
						CGPROGRAM
							DOFURPASS(0.7)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.725"
						CGPROGRAM
							DOFURPASS(0.725)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.75"
						CGPROGRAM
							DOFURPASS(0.75)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.775"
						CGPROGRAM
							DOFURPASS(0.775)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.8"
						CGPROGRAM
							DOFURPASS(0.8)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.825"
						CGPROGRAM
							DOFURPASS(0.825)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.85"
						CGPROGRAM
							DOFURPASS(0.85)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.875"
						CGPROGRAM
							DOFURPASS(0.875)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.9"
						CGPROGRAM
							DOFURPASS(0.9)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.925"
						CGPROGRAM
							DOFURPASS(0.925)
						ENDCG
					}
					Pass 
					{
						Name "Fur 0.95"
						CGPROGRAM
							DOFURPASS(0.95)
						ENDCG 
					}
					Pass
					{
						Name "Fur 0.975"
						CGPROGRAM
							DOFURPASS(0.975)
						ENDCG
					}
	}
	FallBack "Standard"
}