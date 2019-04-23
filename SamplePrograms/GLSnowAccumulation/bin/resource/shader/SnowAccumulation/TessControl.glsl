#version 430

layout (vertices = 4) out;

layout (location = 0) uniform float inInner;
layout (location = 1) uniform float inOuter;

in gl_PerVertex
{
	vec4 gl_Position;
} gl_in[gl_MaxPatchVertices];

layout (location = 0) in vec3 inNormal[gl_MaxPatchVertices];
layout (location = 1) in vec2 inTexCoord[gl_MaxPatchVertices];

out gl_PerVertex
{
	vec4 gl_Position;
} gl_out[];

layout (location = 0) out vec3 outNormal[];
layout (location = 1) out vec2 outTexCoord[];

void main()
{
	gl_out[gl_InvocationID].gl_Position = gl_in[gl_InvocationID].gl_Position;
    outNormal[gl_InvocationID]   = inNormal[gl_InvocationID];
    outTexCoord[gl_InvocationID] = inTexCoord[gl_InvocationID];
	
	gl_TessLevelOuter[0] = inOuter;
	gl_TessLevelOuter[1] = inOuter;
	gl_TessLevelOuter[2] = inOuter;
	gl_TessLevelOuter[3] = inOuter;
	gl_TessLevelInner[0] = inInner;
	gl_TessLevelInner[1] = inInner;
	return;
}