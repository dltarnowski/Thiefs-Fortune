The example scene is using linear color space and post-processing stack v2.

Custom shader: Particles/Particle Surface, Particles/Particle Surface Transparent

Some particle materials are using these custom shaders for fake shading effects without scene lighting. These shaders would be affected by vertex world normal and view direction, but not scene lighting.

Some particle materials are using Unity built-in Standard Surface / Standard Unlit shader.

Some platforms do not support mesh GPU instancing so mesh particles using these built-in particle shaders are not rendered. In this case, you can disable it in 'Particle System' - 'Renderer' - 'Enable Mesh GPU Instancing'.



--- URP ---

For URP, import URP.unitypackage into your URP project, and delete others.