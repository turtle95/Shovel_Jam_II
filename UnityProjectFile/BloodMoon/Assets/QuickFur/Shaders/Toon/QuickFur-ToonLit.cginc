#include "../QuickFur.cginc"

//Init values for main surface shader
sampler2D _MainTex;

struct Input
{
	float2 uv_MainTex : TEXCOORD0;
};

sampler2D _Ramp;
float4 _Color;
FUR_VARS

inline half4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
{
#ifndef USING_DIRECTIONAL_LIGHT
	lightDir = normalize(lightDir);
#endif

	half d = dot(s.Normal, lightDir)*0.5 + 0.5;
	half3 ramp = tex2D(_Ramp, float2(d, d)).rgb;

	half4 c;
	c.rgb = s.Albedo * _LightColor0.rgb * ramp * (atten * 2);
	c.a = s.Alpha;
	return c;
}
#ifndef DOFURPASS
#define DOFURPASS(step)\
						FUR_VERTS_SURF(step)\
						void surf(Input IN, inout SurfaceOutput o)\
						{\
							fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;\
							o.Albedo = c.rgb;\
							o.Alpha = c.a;\
							FUR_LIGHT(o, step)\
						}
#endif