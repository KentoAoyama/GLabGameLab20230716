#ifndef CUSTOM_CELLULARNOISE_INCLUDED
#define CUSTOM_CELLULARNOISE_INCLUDED

#include "Random.hlsl"

float cellularNoise(float2 seed)
{
    float2 ist = floor(seed);
    float2 fst = frac(seed);

    float distance = 5;

    [unroll]
    for (int y = -1; y <= 1; y++)
    {
        [unroll]
        for (int x = -1; x <= 1; x++)
        {
            float2 neighbor = float2(x, y);
            float2 p = 0.5 + 0.5 * sin(_Time.y + 6.2831 * rand2(ist + neighbor));
            float2 diff = neighbor + p - fst;
            distance = min(distance, length(diff));
        }
    }

    return distance;
}

float voronoiNoise(float2 uv)
{
    float2 p = floor(uv);
    float2 f = frac(uv);
    
    float minDist = 1.0;
    float2 nearest;
    
    for(int j = -1; j <= 1; ++j)
    {
        for(int i = -1; i <= 1; ++i)
        {
            float2 g = float2(i, j);
            float2 randomPoint = rand2(p + g);
            float2 diff = g + randomPoint - f;
            float dist = length(diff);
            
            if (dist < minDist)
            {
                minDist = dist;
                nearest = diff;
            }
        }
    }
                
    return minDist;
}

#endif