# N-Body_sim
A simple Unity project on N-Body simulation in a 3D space, where particles behave based on Newton's Law of Universal Gravitation.
The simulation uses Unity's compute shaders to speed up calculations for the force, velocity and position of every particle. This allowed the N-Body simulation to handle a lot more particles in runtime compared to doing all calculations in a C# script.

# Scripts
The MouseOrbit script lets the user click and drag the mouse to orbit the camera around the world origin, and zoom in and out using the scrollwheel. The script was taken and edited from the original MouseOrbitImproved script here: http://wiki.unity3d.com/index.php?title=MouseOrbitImproved
