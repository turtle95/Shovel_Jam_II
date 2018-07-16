#include "../QuickFur.cginc"
#include "UnityCG.cginc"

//Init values for main shader
sampler2D _MainTex;
samplerCUBE _ToonShade;
float4 _MainTex_ST;
float4 _Color;
FUR_VARS

struct appdata 
{
	float4 vertex : POSITION;
	float2 texcoord : TEXCOORD0;
	float3 normal : NORMAL;
};

struct v2f 
{
	float4 pos : SV_POSITION;
	float2 texcoord : TEXCOORD0;
	float3 cubenormal : TEXCOORD1;
	UNITY_FOG_COORDS(2)
};

//Set up surface shader for each fur pass
#ifndef DOFURPASS
#define DOFURPASS(step) \
							\
							v2f vert(appdata v)\
							{\
								v2f o;\
								DO_FUR_VERT_OFFSET(v,step)\
								o.pos = UnityObjectToClipPos(v.vertex);\
								o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);\
								o.cubenormal = mul(UNITY_MATRIX_MV, float4(v.normal, 0));\
								UNITY_TRANSFER_FOG(o, o.pos);\
								return o;\
							}\
							fixed4 frag (v2f i) : SV_Target\
							{\
								fixed4 col = _Color * tex2D(_MainTex, i.texcoord);\
								col.rgb = FUR_BRIGHTNESS(col.rgb, _Occlusion, _Brightness, step)\
								col.a = FUR_ALPHA(col.a, _Density, step)\
								fixed4 cube = texCUBE(_ToonShade, i.cubenormal);\
								fixed4 c = fixed4(2.0f * cube.rgb * col.rgb, col.a);\
								UNITY_APPLY_FOG(i.fogCoord, c);\
								return c;\
							}
#endif