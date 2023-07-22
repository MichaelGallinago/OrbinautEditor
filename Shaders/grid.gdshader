shader_type canvas_item;

uniform vec3 Colour;
uniform vec4 GridColour;
uniform int Size;

void fragment() 
{
	ivec2 position = ivec2(UV / TEXTURE_PIXEL_SIZE * float(Size));
	vec4 pixelColor = texture(TEXTURE, UV);
	
	if (position.x % Size == 0 || position.y % Size == 0)
	{
		pixelColor = GridColour;
	}
	else
	{
		pixelColor.rgb = Colour;
		pixelColor.rgb /= 255.0;
	}
	
	COLOR = pixelColor;
}
