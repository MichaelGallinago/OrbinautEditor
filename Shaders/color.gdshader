shader_type canvas_item;

uniform vec3 Color;

void fragment() 
{
	COLOR = vec4(Color, texture(TEXTURE, UV).a);
}
