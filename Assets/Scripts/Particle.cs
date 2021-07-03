using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Simulation simulation;

    public Vector3 velocity;
    public float mass;

    public void Initialize(ParticleSettings settings)
    {
        simulation = FindObjectOfType<Simulation>();
        float randMass = Random.Range(-settings.randomMassFac, settings.randomMassFac);
        mass = settings.particleMass + randMass * settings.particleMass;
        transform.localScale *= (1 - randMass) * settings.particleRenderSize;

        if (settings.spawnType == ParticleSettings.SpawnType.RandomSphere) {
            transform.position = Random.insideUnitSphere * settings.spawnRad;
            //Vector3 offset = -transform.position.normalized;
            //transform.LookAt(Vector3.zero);
            //Quaternion rotation = Quaternion.LookRotation(offset, transform.right);
            transform.rotation = Random.rotation;
            velocity = transform.forward * Random.Range(0, settings.maxInitialSpeed);
        }
        else if (settings.spawnType == ParticleSettings.SpawnType.PointBurst) {
            transform.position = Vector3.zero;
            transform.rotation = Random.rotation;
            velocity = transform.forward * Random.Range(0, settings.maxInitialSpeed);
        }
    }

    public void Update() {
        Debug.DrawRay (transform.position, velocity * 10, Color.magenta);
    }

    /*public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        // apply force
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep) {
        transform.position += velocity * timeStep;
    }*/
}
