#version 430

struct TParticle
{
    int Status;
    float Radius;
    float PositionX;
    float PositionY;
    float PositionZ;
    float VelocityX;
    float VelocityY;
    float VelocityZ;
};

layout (std430, binding = 0) buffer Point
{
    TParticle Points[];
};

layout (binding = 0, rgba32f) uniform image2D inHeightMap;
layout (location = 0) uniform int inPointCount;
layout (location = 1) uniform float inTimeStep;
layout (location = 2) uniform float inMinHeight;
layout (location = 3) uniform float inMaxHeight;
layout (location = 4) uniform ivec2 inHeightMapSize;

layout (local_size_x = 32, local_size_y = 32, local_size_z = 1) in;

void main(void)
{
    int index = int(gl_GlobalInvocationID.x
      + (gl_GlobalInvocationID.y * (gl_WorkGroupSize.x * gl_NumWorkGroups.x))
      + (gl_GlobalInvocationID.z * ((gl_WorkGroupSize.x * gl_NumWorkGroups.x) * (gl_WorkGroupSize.y * gl_NumWorkGroups.y))));

    if (index >= inPointCount)
        return;
        
    TParticle point = Points[index];
    
    switch (point.Status) {
        case 0: {
            vec3 position = vec3(point.PositionX, point.PositionY, point.PositionZ);
            vec3 velocity = vec3(point.VelocityX, point.VelocityY, point.VelocityZ);
            vec3 acceleration = vec3(0.0, -9.8, 0.0);
                
            const float con_MaxSpeed = 0.1;
            velocity += acceleration * inTimeStep;
            if (length(velocity) > con_MaxSpeed)
                velocity = normalize(velocity) * con_MaxSpeed;
            
            position += velocity * inTimeStep;

            ivec2 texPos = ivec2(int(0.5 + ((position.x + 0.5) * (inHeightMapSize.x - 1))),
                                 int(0.5 + ((position.z + 0.5) * (inHeightMapSize.y - 1))));
            texPos.x = min(inHeightMapSize.x - 1, max(0, texPos.x));
            texPos.y = min(inHeightMapSize.y - 1, max(0, texPos.y));
            float rawHeight = imageLoad(inHeightMap, texPos).x;
            float height = mix(inMinHeight, inMaxHeight, rawHeight);
            
            if (position.y < height) {
                point.Status = 1;
                
                for (int y = max(0, texPos.y - 5); y < min(inHeightMapSize.y - 1, texPos.y + 5); ++y) {
                    for (int x = max(0, texPos.x - 5); x < min(inHeightMapSize.x - 1, texPos.x + 5); ++x) {
                        vec4 h = imageLoad(inHeightMap, ivec2(x, y));
                        h.x += 0.03;
                        imageStore(inHeightMap, ivec2(x, y), vec4(h));
                    }
                }
            }
            
            point.PositionX = position.x;
            point.PositionY = position.y;    
            point.PositionZ = position.z;
            point.VelocityX = velocity.x;
            point.VelocityY = velocity.y;
            point.VelocityZ = velocity.z;
            Points[index] = point;
            break;
            }
        case 1:
            point.Status = 2;
            break;
    }
        
    return;
}