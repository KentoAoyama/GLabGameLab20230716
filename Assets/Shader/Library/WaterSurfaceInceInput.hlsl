#ifndef CUSTOM_WATERSURFACE_INPUT_INCLUDED
#define CUSTOM_WATERSURFACE_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

TEXTURE2D(_MainTex);
SAMPLER(sampler_MainTex);
TEXTURE2D(_NoiseTex);
SAMPLER(sampler_NoiseTex);

CBUFFER_START(UnityPerMaterial)
float4 _MainTex_ST;
float4 _Color;

// Voronoi
float _Range;
float _Scale;
float4 _ShinessColor;

//Blur
float4 _NoiseTex_ST;
float _ScrollSpeed;
float4 _ScrollDir;
float _BlurStrength;
float4 _BlurDir;
CBUFFER_END

#endif