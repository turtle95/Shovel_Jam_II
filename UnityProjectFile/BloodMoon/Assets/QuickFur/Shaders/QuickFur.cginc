#ifndef CLAMPFUROFFSET
#define CLAMPFUROFFSET
inline float4 ClampFurOffset(float4 gravity, float3 normal, float threshold)
{
	float3 proj = dot(gravity.xyz, normal)*normal;

	float3 proj2 = normalize(gravity.xyz);
	float3 proj3 = -faceforward(proj2, proj2, normal);
	proj3 *= length(proj3 + proj2)*0.5;

	gravity.xyz -= proj - lerp(normal*threshold, proj, length(proj));

	return gravity;
}
#endif

#ifndef FUR_DISTMAP
#define FUR_DISTMAP(x) (log(20*x+1)*0.328)
#endif

#ifndef FUR_STEP
#define FUR_STEP 0.05
#endif

#ifndef FUR_CALC
#define FUR_CALC(vertex, normal, length, step, gravity, worldgravity, windSpeed, wind, worldwind, threshold) (vertex + ClampFurOffset(float4(normal * (FUR_DISTMAP(step) + (FUR_STEP)) * (length*0.5), 0) + (lerp(gravity, mul(unity_WorldToObject, gravity), worldgravity) + (sin(_Time.y*windSpeed)*lerp(wind, mul(unity_WorldToObject, wind), worldwind))) * step * 0.1, normal, threshold));
#endif

#ifndef FUR_BRIGHTNESS
#define FUR_BRIGHTNESS(rgb, occlusion, brightness, step) (rgb*(1 - occlusion*(1-(sqrt(half(max(step,0.001))))))*(brightness));
#endif

#ifndef FUR_LAYERS
#define FUR_LAYERS 20
#endif

#ifndef FUR_ALPHA
#define FUR_ALPHA(a,density,step) min(1, a*(density*(20.0/(FUR_LAYERS)))*(1-step));
#endif

#ifndef FUR_VARS
#define FUR_VARS \
		float _Density;\
		float _Occlusion;\
		float _Brightness;\
		float _Length;\
		float4 _Gravity;\
		half _WorldGravity;\
		float4 _Wind;\
		half _WorldWind;\
		half _MinimumThreshold;\
		float _WindSpeed;
#endif

#ifndef DO_FUR_VERT_OFFSET
#define DO_FUR_VERT_OFFSET(v, step) v.vertex.xyz = FUR_CALC(v.vertex.xyz, normalize(v.normal.xyz), _Length, step, _Gravity, _WorldGravity, _WindSpeed, _Wind, _WorldWind, _MinimumThreshold)
#endif

#ifndef FUR_VERTS_SURF
#define FUR_VERTS_SURF(step) \
void vert(inout appdata_full v)\
{\
	DO_FUR_VERT_OFFSET(v, step)\
}
#endif

#ifndef FUR_LIGHT
#define FUR_LIGHT(o, step) \
		o.Albedo = FUR_BRIGHTNESS(o.Albedo, _Occlusion, _Brightness, step)\
		o.Alpha = FUR_ALPHA(o.Alpha, _Density, step)
#endif