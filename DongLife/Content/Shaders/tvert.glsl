#version 400 core
in vec2 pos;

out vec2 TexCoords;

uniform mat4 proj;
uniform mat4 model;

void main()
{
	TexCoords = vec2(pos.x, -pos.y); //FBO correction
	gl_Position = proj * model * vec4(pos, 0, 1);
}