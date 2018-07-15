Shader "QuickFur/10-Step/Cutout/Unlit" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
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
						#pragma vertex vert
						#pragma fragment frag
						#pragma multi_compile_fog

						#define FUR_STEP 0.1

						#include "QuickFur-UnlitCutout.cginc"
					ENDCG

					//Run 10 fur passes
					Pass 
					{ 
						CGPROGRAM
							DOFURPASS(0)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.1)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.2)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.3)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.4)
						ENDCG 
					}
					Pass
					{
						CGPROGRAM
							DOFURPASS(0.5)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.6)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.7)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.8)
						ENDCG 
					}
					Pass 
					{
						CGPROGRAM
							DOFURPASS(0.9)
						ENDCG 
					}
	}
	FallBack "Standard"
}