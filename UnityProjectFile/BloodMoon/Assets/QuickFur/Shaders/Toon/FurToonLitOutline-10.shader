Shader "QuickFur/10-Step/Toon Lit Outline" {
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(.002, 0.03)) = .005
		_MainTex("Base (RGB)", 2D) = "white" { }
		_Ramp("Toon Ramp (RGB)", 2D) = "gray" {}
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

	SubShader{
		Tags { "RenderType"="Opaque" }

		UsePass "QuickFur/10-Step/Toon Basic Outline/OUTLINE"
		
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off
					CGINCLUDE
						#pragma surface surf ToonRamp vertex:vert alpha:fade
						#pragma lighting ToonRamp exclude_path:prepass
			
						#define FUR_STEP 0.1
						#define FUR_LAYERS 10

						#include "QuickFur-ToonLit.cginc"
					ENDCG

					//Run 10 fur passes
					CGPROGRAM
						DOFURPASS(0)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.1)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.2)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.3)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.4)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.5)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.6)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.7)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.8)
					ENDCG
					CGPROGRAM
						DOFURPASS(0.9)
					ENDCG
	} 
	
	Fallback "Standard"
}
