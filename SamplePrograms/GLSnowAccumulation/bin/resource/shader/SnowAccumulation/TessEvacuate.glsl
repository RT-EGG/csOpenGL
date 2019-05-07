#version 430

layout (quads, equal_spacing, ccw) in;

layout (binding = 0)  uniform sampler2D inHeightMap;
layout (location = 2) uniform mat4 inProjectionMatrix;
layout (location = 3) uniform mat4 inModelViewMatrix;
layout (location = 4) uniform mat4 inModelMatrix;
layout (location = 5) uniform mat3 inNormalMatrix;
layout (location = 6) uniform float inMinHeight;
layout (location = 7) uniform float inMaxHeight;

in gl_PerVertex
{
	vec4 gl_Position;
}gl_in[gl_MaxPatchVertices];

layout (location = 0) in vec3 inNormal[gl_MaxPatchVertices];
layout (location = 1) in vec2 inTexCoord[gl_MaxPatchVertices];

out gl_PerVertex
{
	vec4 gl_Position;
};

layout (location = 0) out vec3 outNormal;
layout (location = 1) out vec2 outSurfaceTexCoord;
layout (location = 2) out float outOffset;

vec4 CalcPositionAt(vec3 aTessCoord)
{
    vec4 p0 = mix( gl_in[0].gl_Position, gl_in[1].gl_Position, aTessCoord.x );
	vec4 p1 = mix( gl_in[3].gl_Position, gl_in[2].gl_Position, aTessCoord.x );
    return mix( p0, p1, aTessCoord.y );
}

vec2 CalcTexCoordAt(vec3 aTessCoord)
{
    vec2 t0 = mix( inTexCoord[0], inTexCoord[1], aTessCoord.x );
    vec2 t1 = mix( inTexCoord[3], inTexCoord[2], aTessCoord.x );
    return mix( t0, t1, aTessCoord.y );
}

vec3 CalcNormalAt(vec3 aTessCoord)
{
    vec3 n0 = mix( inNormal[0], inNormal[1], aTessCoord.x );
    vec3 n1 = mix( inNormal[3], inNormal[2], aTessCoord.x );
    return mix( n0, n1, aTessCoord.y );
}

vec4 CalcOffsetedPositionAt(vec3 aTessCoord, out float aOffset)
{
    vec4 position = CalcPositionAt(aTessCoord);
    vec2 texCoord = CalcTexCoordAt(aTessCoord); 
    vec3 normal   = CalcNormalAt(aTessCoord);
    
    float x = (texCoord.x - 0.5) * 2.0;
    float y = (texCoord.y - 0.5) * 2.0;
    float d = mix(inMinHeight, inMaxHeight, texture(inHeightMap, texCoord).x);
    
    position.xyz += normal * d;
    aOffset = d;
    return position;
}

vec4 CalcOffsetedPositionAt(vec3 aTessCoord)
{
    float dummy = 0.0;
    return CalcOffsetedPositionAt(aTessCoord, dummy);
}

void main()
{
    float offset = 0.0;
    gl_Position = inProjectionMatrix * inModelViewMatrix * CalcOffsetedPositionAt(gl_TessCoord, offset);

    const float DELTA = 0.01;
    vec4 px0 = normalize(CalcOffsetedPositionAt(gl_TessCoord + vec3(-DELTA, 0.0, 0.0)));
    vec4 px1 = normalize(CalcOffsetedPositionAt(gl_TessCoord + vec3(+DELTA, 0.0, 0.0)));
    vec4 py0 = normalize(CalcOffsetedPositionAt(gl_TessCoord + vec3(0.0, -DELTA, 0.0)));
    vec4 py1 = normalize(CalcOffsetedPositionAt(gl_TessCoord + vec3(0.0, +DELTA, 0.0)));
    
    outNormal = normalize(cross(px1.xyz - px0.xyz, py1.xyz - py0.xyz));
    outNormal = inNormalMatrix * outNormal;
    
    outSurfaceTexCoord = CalcTexCoordAt(gl_TessCoord);
    outOffset = offset;

	return;
}