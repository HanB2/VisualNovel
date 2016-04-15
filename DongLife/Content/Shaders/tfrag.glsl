#version 400 core
in vec2 TexCoords;

out vec4 outColor;

uniform sampler2D baseTex;
uniform sampler2D destTex;
uniform sampler2D tranTex;

void main()
{
	vec4 sampleColor = texture(tranTex, TexCoords);
	if (sampleColor.r == 0)
		outColor = texture(baseTex, TexCoords);
	else
		outColor = texture(destTex, TexCoords);
}