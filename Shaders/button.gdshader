shader_type canvas_item;

const vec3 BaseColor = vec3(224, 224, 224);
const vec3 PressColor = vec3(113, 188, 252);
uniform bool IsPressed;

void fragment() 
{
	vec4 pixelColor = texture(TEXTURE, UV);
	pixelColor.rgb = (IsPressed ? PressColor : BaseColor) / 255.;
	if (pixelColor.a == 0.0)
	{
		pixelColor.a = IsPressed ? 0.2 : 0.05;
	}
	COLOR = pixelColor;
}
