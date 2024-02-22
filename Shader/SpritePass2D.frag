#version 460 core

layout(location = 0) in vec2 inTexCoords;

layout(location = 0) out vec4 outFragColor;

layout(binding = 0) uniform sampler2D spriteMap;

const float gamma = 2.2f;

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
	vec3 fragRGB = vec3(0.0f);

	if(!bIsActiveOutline)
	{
		fragRGB = texture(spriteMap, inTexCoords).rgb;
		fragRGB = pow(fragRGB, vec3(1.0f / gamma));

		float alpha = texture(spriteMap, inTexCoords).a;
		alpha *= transparent;
		
		outFragColor = vec4(fragRGB, alpha);

	}
	else
	{
//		vec2 size = 1.0f / textureSize(spriteMap, 0);
//		float outline = 0.0f;
//		for(int index = 0; index < offsets.length(); ++index)
//		{
//			outline += texture(spriteMap, inTexCoords + size * offsets[index]).a;
//		}
//
//		outline = min(outline, 1.0f);
//
//		fragRGB = texture(spriteMap, inTexCoords).rgb;
//		float alpha = texture(spriteMap, inTexCoords).a;
//
//		fragRGB = mix(fragRGB, outlineRGBA.rgb, outline - alpha);
//		fragRGB = pow(fragRGB, vec3(1.0f / gamma));
//
//		alpha *= (transparent * outlineRGBA.a);
//
//		outFragColor = vec4(fragRGB, alpha);

		vec2 size = 1.0f / textureSize(spriteMap, 0);
		float outline = 0.0f;
		for(int index = 0; index < offsets.length(); ++index)
		{
			outline += texture(spriteMap, inTexCoords + size * offsets[index]).a;
		}

		outline = min(outline, 1.0f);
		
		vec4 colorRGBA = texture(spriteMap, inTexCoords);
		outFragColor = mix(colorRGBA, outlineRGBA, outline - colorRGBA.a);
	}
}