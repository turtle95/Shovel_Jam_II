#include "../QuickFur.cginc"

//Init values for main surface shader
sampler2D _MainTex;

struct Input
{
	float2 uv_MainTex;
};

half _Glossiness;
half _Metallic;
fixed4 _Color;
FUR_VARS

//Set up surface shader for each fur pass
#ifndef DOFURPASS
#define DOFURPASS(step)\
							FUR_VERTS_SURF(step)\
							void surf(Input IN, inout SurfaceOutputStandard o)\
							{\
								fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;\
								o.Albedo = c.rgb;\
								o.Metallic = _Metallic;\
								o.Smoothness = _Glossiness;\
								o.Alpha = c.a;\
								FUR_LIGHT(o, step)\
							}
#endif