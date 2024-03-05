#version 460 core

layout(location = 0) in vec4 inColor;

layout(location = 0) out vec4 outFragColor;

void main()
{
	vec3 fragRGB = inColor.rgb;
	outFragColor = vec4(fragRGB, inColor.a);
}