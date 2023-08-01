shader_type canvas_item;

uniform vec3 Color;
uniform vec4 GridColor;
uniform int Size;

void fragment() 
{
	ivec2 position = ivec2(UV / TEXTURE_PIXEL_SIZE * float(Size));
	vec4 pixelColor = texture(TEXTURE, UV);
	
	if (position.x % Size == 0 || position.y % Size == 0)
	{
		pixelColor = GridColor;
	}
	else
	{
		pixelColor.rgb = Color;
		pixelColor.rgb /= 255.0;
	}
	
	COLOR = pixelColor;
}
