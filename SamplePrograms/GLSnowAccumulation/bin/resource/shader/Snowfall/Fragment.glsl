#version 460

flat in int vDiscard;
out vec4 outColor;

void main()
{
    if (vDiscard != 0)
        discard;
        
    float len = length((gl_PointCoord - vec2(0.5, 0.5)) * 2.0);
    outColor.xyz = vec3(1.0);
    outColor.w   = 1.0 - len;
    if (outColor.w < 0.01)
        discard;
    return;
}