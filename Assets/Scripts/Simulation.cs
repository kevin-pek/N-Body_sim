using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public ParticleSettings settings;
    public ComputeShader compute;
    public Particle prefab;
    Particle[] particles;

    void Awake()
    {
        for (int i = 0; i < settings.spawnCount; i++) {
            Particle particle = Instantiate (prefab);
        }
    }

    void Start() {
        particles = FindObjectsOfType<Particle>();
        foreach (Particle particle in particles) {
            particle.Initialize(settings);
        }
    }

    void FixedUpdate() {
        SimulationStep();
    }

    void SimulationStep() {
        // using compute shaders
        int numParticles = particles.Length;
        var particleData = new ParticleData[numParticles];

        for (int i = 0; i < particles.Length; i++) {
            particleData[i].position = particles[i].transform.position;
            particleData[i].mass = particles[i].mass;
            particleData[i].velocity = particles[i].velocity;
        }

        var particleBuffer = new ComputeBuffer(numParticles, ParticleData.Size);
        particleBuffer.SetData(particleData);

        compute.SetBuffer(0, "particles", particleBuffer);
        compute.SetInt("numParticles", particles.Length);
        compute.SetFloat("timeStep", Time.fixedDeltaTime * settings.timeScale);

        int threadGroups = Mathf.CeilToInt (numParticles / (float) threadGroupSize);
        compute.Dispatch (0, threadGroups, 1, 1);

        particleBuffer.GetData(particleData);
        particleBuffer.Release();

        /*for (int i = 0; i < particles.Length; i++) {
            //Vector3 a = particleData[i].accelerationMagnitude * particleData[i].accelerationDir.normalized;
            particles[i].UpdateVelocity(particleData[i].acceleration, Time.fixedDeltaTime);
        }*/

        for (int i = 0; i < particles.Length; i++) {
            particles[i].transform.position = particleData[i].position;
            particles[i].velocity = particleData[i].velocity;
        }
/*
        // slow generic method to calc forces for each particle
        for (int i = 0; i < particles.Length; i++) {
            Vector3 acceleration = Vector3.zero;

            for (int j = 0; j < particles.Length; j++) {
                if (j != i) {
                    Vector3 forceDir = (particles[j].transform.position - particles[i].transform.position).normalized;
                    float sqrDst = (particles[j].transform.position - particles[i].transform.position).sqrMagnitude;
                    acceleration += forceDir * gConstant * particles[j].mass / sqrDst;
                }
            }
            
            particles[i].UpdateVelocity(acceleration, Time.fixedDeltaTime);
        }

        foreach (Particle particle in particles) {
            particle.UpdatePosition(Time.fixedDeltaTime);
        }*/
    }

    Vector3 GetMeanVector(List<Vector3> positions){
        if (positions.Count == 0)
            return Vector3.zero;
        
        float x = 0f;
        float y = 0f;
        float z = 0f;

        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }
}

public struct ParticleData {
    //passed values
    public Vector3 position;
    public float mass;
    public Vector3 velocity;
    //returned values
    public Vector3 acceleration;

    public static int Size {
        get {
            return sizeof (float) * 3 * 3 + sizeof (float);
        }
    }
}