using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ParticleSettings : ScriptableObject
{
    public enum SpawnType {
        PointBurst,
        RandomSphere,
    }

    [Header("Particle Settings")]
    public float particleMass = 10000;
    public float randomMassFac = 0;
    public float particleRenderSize = 1;
    public Vector3 initialVelocity = Vector3.zero;
    public Vector3 initialPosition = Vector3.zero;

    [Header("Simulation Settings")]
    [MinAttribute(0)]public int timeScale = 100;
    public SpawnType spawnType;    
    public float spawnRad = 10;
    public int spawnCount = 3000;
}
