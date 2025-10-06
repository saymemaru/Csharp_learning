#version 330 core

in vec3 colour;

out vec4 out_color;

void main()
{
    //out_color = vec4(1.0, 0.5, 0.2, 1.0);
    out_color = vec4(colour, 1.0);
}