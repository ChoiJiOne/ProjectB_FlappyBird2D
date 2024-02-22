#version 460 core

layout(location = 0) in vec4 inColor;

layout(location = 0) out vec4 outFragColor;

const float gamma = 2.2f;

void main()
{
	vec3 fragRGB = inColor.rgb;
	fragRGB = pow(fragRGB, vec3(1.0f / gamma));

	outFragColor = vec4(fragRGB, inColor.a);
}