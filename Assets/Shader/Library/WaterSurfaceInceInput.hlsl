#ifndef CUSTOM_WATERSURFACE_INPUT_INCLUDED
#define CUSTOM_WATERSURFACE_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);

CBUFFER_START(UnityPerMaterial)
float4 _MainTex_ST;
float4 _Color;
CBUFFER_END

#endif