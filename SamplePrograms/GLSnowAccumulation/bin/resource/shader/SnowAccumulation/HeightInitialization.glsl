#version 430

layout (binding = 0, rgba32f) uniform image2D inDstTexture;
layout (location = 1) uniform int inTextureWidth;
layout (location = 2) uniform int inTextureHeight;
layout (location = 3) uniform float inRandomSeed;

layout (local_size_x = 32, local_size_y = 32, local_size_z = 1) in;

float rand(vec2 co, float aSeed)
{
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233)) * float(aSeed)) * 43758.5453);
}

float rand(vec2 co)
{
    return rand(co, inRandomSeed);
}

float fade(float aY0, float aY1, float aRatio)
{
    float ratio = (6.0 * pow(aRatio, 5.0)) - (15.0 * pow(aRatio, 4.0)) + (10.0 * pow(aRatio, 3.0));
    return ((1.0 - ratio) * aY0) + (ratio * aY1);
}

void main(void)
{
    uint index =  gl_GlobalInvocationID.x
      + (gl_GlobalInvocationID.y * (gl_WorkGroupSize.x * gl_NumWorkGroups.x))
      + (gl_GlobalInvocationID.z * ((gl_WorkGroupSize.x * gl_NumWorkGroups.x) * (gl_WorkGroupSize.y * gl_NumWorkGroups.y)));

    uint y = index / inTextureWidth;
    uint x = index % inTextureWidth;
    if ((inTextureWidth <= x) || (inTextureWidth <= y))
        return;
        
    const int DIVISION = 4;
    const float CELL_SIZE = 1.0 / DIVISION;
    const int MAX_INDEX = DIVISION - 1;
    vec2 coord = vec2(float(x) / float(inTextureWidth), 
                      float(y) / float(inTextureHeight));
    vec2 pos = vec2(coord.x / CELL_SIZE, coord.y / CELL_SIZE); 
                    
    vec2 ratio = vec2(fract(pos.x), fract(pos.y));
    pos.x = floor(pos.x);
    pos.y = floor(pos.y);
    
    float x0y0 = rand(pos + vec2(0.0, 0.0)); 
    float x1y0 = rand(pos + vec2(1.0, 0.0));
    float x0y1 = rand(pos + vec2(0.0, 1.0));
    float x1y1 = rand(pos + vec2(1.0, 1.0));
    
    float value = fade(fade(x0y0, x1y0, ratio.x),
                       fade(x0y1, x1y1, ratio.x),
                       ratio.y);

    imageStore(inDstTexture, ivec2(x, y), vec4(vec3(value), 1.0));
    return;
}