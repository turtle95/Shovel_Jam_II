Shader "QuickFur/10-Step/Toon Basic Outline" {
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

		Pass
		{
			Tags{ "RenderType" = "Opaque" }
			Name "OUTLINE"
			Tags{ "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
				#pragma vertex vert_outline
				#pragma fragment frag_outline
				#pragma multi_compile_fog
				#include "UnityCG.cginc"
			
				#define FUR_STEP 0.1
				#define FUR_LAYERS 10

				#include "../QuickFur.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
				};

				struct v2f
				{
					float4 pos : SV_POSITION;
					UNITY_FOG_COORDS(0)
						fixed4 color : COLOR;
				};

				uniform float _Outline;
				uniform float4 _OutlineColor;
				FUR_VARS

				v2f vert_outline(appdata v)
				{
					v2f o; 
					DO_FUR_VERT_OFFSET(v, 0.9)
					o.pos = UnityObjectToClipPos(v.vertex);

					float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
					float2 offset = TransformViewToProjection(norm.xy);

					#ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE //to handle recent standard asset package on older version of unity (before 5.5)
								o.pos.xy += offset * UNITY_Z_0_FAR_FROM_CLIPSPACE(o.pos.z) * _Outline;
					#else
								o.pos.xy += offset * o.pos.z * _Outline;
					#endif
					o.color = _OutlineColor;
					UNITY_TRANSFER_FOG(o,o.pos);
					return o;
				}
				fixed4 frag_outline(v2f i) : SV_Target
				{
					UNITY_APPLY_FOG(i.fogCoord, i.color);
					return i.color;
				}
			ENDCG
		}
		
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 200
		ZWrite Off

		UsePass "QuickFur/10-Step/Toon Basic/FUR 0"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.1"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.2"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.3"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.4"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.5"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.6"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.7"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.8"
		UsePass "QuickFur/10-Step/Toon Basic/FUR 0.9"

		
	}
	
	Fallback "Toon/Basic"
}
