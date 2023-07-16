#ifndef CUSTOM_RANDOM_INCLUDED
#define CUSTOM_RANDOM_INCLUDED

float rand (float2 seed)
{
    return frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
}

float noise (float2 st)
{
    float2 p = floor(st);
    return rand(p);
}

float2 rand2 (float2 seed)
{
    seed = float2(dot(seed, float2(127.1, 311.7)), dot(seed, float2(269.5, 183.3)));
    return -1.0 + 2.0 * frac(sin(seed) * 43758.5453123);
}

float perlinNoise (float2 seed)
{
    float2 p = floor(seed);
    float2 f = frac(seed);
    float2 u = f * f * (3.0 - 2.0 * f);

    float v00 = rand2(p + float2(0, 0));
    float v10 = rand2(p + float2(1, 0));
    float v01 = rand2(p + float2(0, 1));
    float v11 = rand2(p + float2(1, 1));

    return lerp(lerp(dot(v00, f - float2(0, 0)), dot(v10, f - float2(1, 0)), u.x),
                lerp(dot(v01, f - float2(0, 1)), dot(v11, f - float2(1, 1)), u.x),
                u.y) + 0.5f;
}

#endif