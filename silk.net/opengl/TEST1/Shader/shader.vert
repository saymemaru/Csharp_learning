//Here we specify the version of our shader.
#version 330 core
//These lines specify the location and type of our attributes,
//the attributes here are prefixed with a "v" as they are our inputs to the vertex shader
//this isn't strictly necessary though, but a good habit.
in vec3 position;
in vec2 textureCoords;

out vec2 pass_textureCoords;

void main()
{
    gl_Position = vec4(position, 1.0);
    pass_textureCoords = textureCoords;
}