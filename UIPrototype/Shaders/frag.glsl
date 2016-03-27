#version 400 core
in vec2 TexCoords;

out vec4 outColor;

uniform sampler2D image;
uniform bool blurImage;
uniform bool desaturateImage;
uniform vec4 drawColor = vec4(1, 1, 1, 1);
uniform vec2 resolution;

vec3 desaturate(vec3 color, float amount)
{
	vec3 saturateValues = vec3(0.2126, 0.7152, 0.0722);
	vec3 gray = vec3(dot(saturateValues, color));

	return vec3(mix(color, gray, amount));
}

vec4 blurPoint(vec2 samplePoint, float xstep, float ystep)
{
	vec4 color00 = texture(image, samplePoint);
	vec4 color01 = texture(image, vec2(samplePoint.x + xstep, samplePoint.y + ystep));
	vec4 color02 = texture(image, vec2(samplePoint.x + xstep, samplePoint.y - ystep));
	vec4 color03 = texture(image, vec2(samplePoint.x + xstep, samplePoint.y));
	vec4 color04 = texture(image, vec2(samplePoint.x, samplePoint.y + ystep));
	vec4 color05 = texture(image, vec2(samplePoint.x, samplePoint.y - ystep));
	vec4 color06 = texture(image, vec2(samplePoint.x - xstep, samplePoint.y + ystep));
	vec4 color07 = texture(image, vec2(samplePoint.x - xstep, samplePoint.y - ystep));
	vec4 color08 = texture(image, vec2(samplePoint.x - xstep, samplePoint.y));

	return (color00 + color01 + color02 + color03 + color04 + color05 + color06 + color07 + color08) / 9;
}

vec4 blur()
{
	float xstep = 1.0 / resolution.x;
	float ystep = 1.0 / resolution.y;

	vec4 color01 = blurPoint(vec2(TexCoords.x + xstep * 2.0, TexCoords.y), xstep, ystep);
	vec4 color02 = blurPoint(vec2(TexCoords.x - xstep * 2.0, TexCoords.y), xstep, ystep);
	vec4 color03 = blurPoint(vec2(TexCoords.x, TexCoords.y + ystep * 2.0), xstep, ystep);
	vec4 color04 = blurPoint(vec2(TexCoords.x, TexCoords.y - ystep * 2.0), xstep, ystep);

	return (color01 + color02 + color03 + color04) / 4;
}

void main()
{
	//For vertical/horizontal mirroring (later)
	vec2 samplePoint = TexCoords.xy;
	outColor = texture(image, samplePoint);

	if (blurImage)
		outColor = blur();
	if (desaturateImage)
		outColor = vec4(desaturate(outColor.rgb, 0.6), outColor.a);

	outColor = outColor * drawColor;
}