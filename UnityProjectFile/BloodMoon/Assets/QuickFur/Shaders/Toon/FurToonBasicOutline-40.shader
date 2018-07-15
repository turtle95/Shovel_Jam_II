Shader "QuickFur/40-Step/Toon Basic Outline" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
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

	SubShader {

		CGINCLUDE
			#define FUR_STEP 0.025
			#define FUR_LAYERS 20
		ENDCG

		UsePass "QuickFur/10-Step/Toon Basic Outline/OUTLINE"
		
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off

		UsePass "QuickFur/40-Step/Toon Basic/FUR 0"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.025"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.05"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.75"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.1"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.125"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.15"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.175"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.2"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.225"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.25"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.275"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.3"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.325"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.35"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.375"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.4"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.425"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.45"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.475"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.5"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.525"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.55"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.575"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.6"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.625"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.65"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.675"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.7"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.725"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.75"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.775"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.8"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.825"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.85"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.875"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.9"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.925"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.95"
		UsePass "QuickFur/40-Step/Toon Basic/FUR 0.975"

		
	}
	
	Fallback "Toon/Basic"
}
