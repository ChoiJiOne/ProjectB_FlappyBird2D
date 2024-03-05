#version 460 core

layout(location = 0) in vec2 inTexCoords;

layout(location = 0) out vec4 outFragColor;

layout(binding = 0) uniform sampler2D spriteMap;

const vec2 offsets[8] = 
{
	vec2(-1.0f, -1.0f),
	vec2(-1.0f, +0.0f),
	vec2(-1.0f, +1.0f),
	vec2(+0.0f, -1.0f),
	vec2(+0.0f, +1.0f),
	vec2(+1.0f, -1.0f),
	vec2(+1.0f, +0.0f),
	vec2(+1.0f, +1.0f),
};

uniform bool bIsActiveOutline;
uniform float transparent;
uniform vec4 outlineRGBA;

void main()
{
	vec4 fragColor = vec4(0.0f);

	if(bIsActiveOutline)
	{
		vec2 size = 1.0f / textureSize(spriteMap, 0);
		float outline = 0.0f;
		for(int index = 0; index < offsets.length(); ++index)
		{
			outline += texture(spriteMap, inTexCoords + size * offsets[index]).a;
		}

		outline = min(outline, 1.0f);
		
		fragColor = texture(spriteMap, inTexCoords);
		fragColor = mix(fragColor, outlineRGBA, outline - fragColor.a);
	}
	else
	{
		fragColor = texture(spriteMap, inTexCoords);
	}
	
	vec3 fragRGB = fragColor.rgb;
	float alpha = fragColor.a * transparent;

	outFragColor = vec4(fragRGB, alpha);
}