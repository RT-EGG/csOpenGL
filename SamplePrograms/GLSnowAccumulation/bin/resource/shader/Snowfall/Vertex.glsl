#version 430

// vertex shader

layout (location = 0) uniform mat4 inProjectionMatrix;
layout (location = 1) uniform mat4 inModelviewMatrix;
layout (location = 2) uniform ivec2 inViewportSize;

layout (location = 0) in int inStatus;
layout (location = 1) in float inRadius;
layout (location = 2) in vec3 inPosition;

out gl_PerVertex
{
	vec4 gl_Position;
    float gl_PointSize;
};

flat out int vDiscard;

void main()
{
    gl_Position = inProjectionMatrix * inModelviewMatrix * vec4(inPosition, 1.0);
    gl_PointSize = float(inViewportSize.y) * inProjectionMatrix[1][1] * (inRadius / gl_Position.w);
    
    vDiscard = inStatus == 0 ? 0 : 1;
	
    return;
}