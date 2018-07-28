Shader "QuickFur/05-Step/Unlit" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
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
			
						#define FUR_STEP 0.2
						#define FUR_LAYERS 5

						#include "QuickFur-Unlit.cginc"
					ENDCG

					//Run 5 fur passes
					Pass 
					{ 
						CGPROGRAM
							DOFURPASS(0)
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
							DOFURPASS(0.4)
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
							DOFURPASS(0.8)
						ENDCG 
					}
	}
	FallBack "Standard"
}