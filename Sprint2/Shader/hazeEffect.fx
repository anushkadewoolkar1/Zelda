/*

// this isn't the shader file being read, the real one is in Content but this is just so is easier to find. (contents of file are the same).

// *NOTE* if you can't build this with the mgcb file (it may something like a dll can't be found) you need to install 
// Visual C++ Redistributable Packages for Visual Studio 2013 because of a monogame bug they have known about but not fixed for many years
// https://www.microsoft.com/en-gb/download/details.aspx?id=40784 x86

// t0 and s0 is what monogames descriptor set binds the texture/sampler to by default
texture Texture : register(t0);
sampler TextureSampler : register(s0);

// time changes constantly, everything else is fixed
float Time;
float2 Resolution;
float HazeIntensity = 0.02;
float HazeSpeed = 1.5;
float HazeFrequency = 15.0;

// Pixel shader
float4 PixelShaderFunction(float2 uv : TEXCOORD0) : COLOR0
{
    // get game colors.
    float4 sceneColor = tex2D(TextureSampler, uv);

    // only do the fog in the top 73% of screen to avoid overlapping hud
    if (uv.y > 0.73) 
        return sceneColor;

    // uv is normalized 0-1 so if we multiply that by the resolution, we get the position in pixel coordinates
    float2 pos = uv * Resolution;

    // noise. each tile is 2x2, moves 30px per second. last, fetch which tile the pixel is belong to (since pixel shaders go one by one each pixel)
    float tileSize = 2.0;
    float speed = 30.0;
    float movedX = pos.x - Time * speed;

    // every pixel reads a tile the same shifted amount in relation to the other pixels(based on time) 
    float2 tileCoord = float2(
        floor(movedX / tileSize),
        floor(pos.y / tileSize)
    );

    // psuedo random value normalized for each tile (each tile has fixed color)
    float rnd = frac(sin(dot(tileCoord, float2(12.9898, 78.233))) * 43758.5453);

    // intensity
    float fogFactor = rnd * 0.8;
    
    float4 fogColor = float4(0.9, 0.9, 0.95, 1.0);
    return lerp(sceneColor, fogColor, fogFactor);

}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

*/