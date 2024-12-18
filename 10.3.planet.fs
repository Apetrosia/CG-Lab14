#version 330 core
out vec4 FragColor;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;

uniform sampler2D texture_diffuse1;
uniform vec3 lightDir; // Directional light direction in world space
uniform vec3 lightColor; // Light color
uniform vec3 viewPos; // Camera position
uniform vec3 objectColor; // Base color of the object

void main()
{
    lightColor = vec3(1.0, 1.0, 1.0);
    lightDir = vec3(1.0, 1.0, 1.0);
    
    // Normalize inputs
    vec3 norm = normalize(Normal);
    vec3 lightDirection = normalize(-lightDir); // Light direction

    // Lambertian shading (diffuse lighting)
    float diff = max(dot(norm, lightDirection), 0.0);

    // Toon shading steps
    float toonLevels = 4.0;
    float toonFactor = floor(diff * toonLevels) / toonLevels;

    vec3 diffuse = toonFactor * lightColor * objectColor;

    // Texture application
    vec3 texColor = texture(texture_diffuse1, TexCoords).rgb;
    vec3 resultColor = diffuse * texColor;

    FragColor = vec4(resultColor, 1.0f);
}