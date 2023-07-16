#ifndef CUSTOM_VORONOI_INCLUDED
#define CUSTOM_VORONOI_INCLUDED

#include "Random.hlsl"

float voronoi (float2 seed)
{
    float2 p = floor (seed);
    float2 f = frac(seed);

    float2 res = float2(8, 8);
    [unroll]
    for (int i = -1; i <= 1; i++)
    {
        [unroll]
        for (int j = -1; j <= 1; j++)
        {
            float2 b = float2(i, j);
            float2 r = b - f + rand2(p + b);

            float d = max(abs(r.x), abs(r.y));

            if (d < res.x)
            {
                res.y = res.x;
                res.x = d;
            }
            else if (d < res.y)
            {
                res.y = d;
            }
        }
    }

    return res.y - res.x;
}

#endif