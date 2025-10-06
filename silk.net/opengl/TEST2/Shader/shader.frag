//Specifying the version like in our vertex shader.
#version 330 core
in vec2 pass_textureCoords;

out vec4 out_Color;

uniform sampler2D textureSampler;

void main()
{
    //Here we are setting our output variable, for which the name is not important.
    out_Color = texture(textureSampler,pass_textureCoords);
}