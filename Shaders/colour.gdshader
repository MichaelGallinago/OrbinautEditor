shader_type canvas_item;

uniform vec3 Colour;

void fragment() 
{
	vec4 pixelColor = texture(TEXTURE, UV);
	pixelColor.rgb = Colour;
	COLOR = pixelColor;
}
