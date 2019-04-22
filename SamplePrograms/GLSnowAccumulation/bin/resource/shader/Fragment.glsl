#version 460

// fragment shader

struct TMaterial
{
    vec4 Ambient;
    vec4 Diffuse;
    vec4 Specular;
    vec4 Emission;
    float Shininess;
};

struct TLightMaterial
{
    vec4 Ambient;
    vec4 Diffuse;
    vec4 Specular;
};

struct TLightInstance
{
    vec3 Position;
    vec3 Direction;
    TLightMaterial Material;
};

//layout (binding = 0)  uniform sampler2D inHeightMap;
layout (location = 9) uniform ivec2 inHeightMapSize;

layout (std140) uniform Material
{
    TMaterial FrontMaterial;
};

layout (std430, binding = 1) buffer Light
{
    TLightInstance Lights[];
};

layout (binding = 1)  uniform sampler2D inSurfaceTexture;
layout (location = 0) in vec3 inNormal;
layout (location = 1) in vec2 inSurfaceTexCoord;
layout (location = 2) in float inOffset;
layout (location = 0) out vec4 outFragColor;

void main()
{
    outFragColor = vec4(0.0, 0.0, 0.0, 1.0);
    for (int i = 0; i < 1; ++i) {
        float diffusion = dot(inNormal, -Lights[i].Direction);
    
        outFragColor += (FrontMaterial.Ambient * Lights[i].Material.Ambient)
                      + (FrontMaterial.Diffuse * Lights[i].Material.Diffuse * diffusion);
    }
    outFragColor *= texture(inSurfaceTexture, inSurfaceTexCoord);
    //outFragColor.x = inSurfaceTexCoord.x / 1024.0;
    //outFragColor.y = inSurfaceTexCoord.y / 1024.0;
    outFragColor.w = 1.0;
    return;    
}