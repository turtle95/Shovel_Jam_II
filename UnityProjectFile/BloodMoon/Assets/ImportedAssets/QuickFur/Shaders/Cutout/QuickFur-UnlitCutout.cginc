#include "../QuickFur.cginc"
#include "UnityCG.cginc"

//Init values for main shader
sampler2D _MainTex;
float4 _MainTex_ST;
float4 _Color;
half _Cutoff;
FUR_VARS

struct appdata
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float3 normal : NORMAL;
};

struct v2f
{
	float2 uv : TEXCOORD0;
	UNITY_FOG_COORDS(1)
	float4 vertex : SV_POSITION;
};

//Set up surface shader for each fur pass
#ifndef DOFURPASS
#define DOFURPASS(step) \
							v2f vert (appdata v)\
							{\
								v2f o;\
								DO_FUR_VERT_OFFSET(v, step)\
								o.vertex = UnityObjectToClipPos(v.vertex);\
								o.uv = TRANSFORM_TEX(v.uv, _MainTex);\
								UNITY_TRANSFER_FOG(o, o.vertex);\
								return o;\
							}\
							fixed4 frag(v2f i) : SV_Target\
							{\
								fixed4 col = tex2D(_MainTex, i.uv);\
								col.rgb = FUR_BRIGHTNESS(col.rgb, _Occlusion, _Brightness, step)\
								col.a = FUR_ALPHA(col.a, _Density, step)\
								clip(col.a - _Cutoff);\
								UNITY_APPLY_FOG(i.fogCoord, col);\
								return col;\
							}
#endif