using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundParticleSystem : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem ground;

    [SerializeField]
    private int numParticles = 100;

    [SerializeField]
    private float minSize = 0.1f;
    [SerializeField]
    private float maxSize = 0.1f;

    void CreateParticles()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[numParticles];
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = MakeParticle(Random(new Vector3(-100f, -100f, 0f), new Vector3(100f, 100f, 1f)));
        }
        ground.SetParticles(particles, particles.Length);
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateParticles();
    }

    public Vector3 Random(Vector3 min, Vector3 max)
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }

    ParticleSystem.Particle MakeParticle(Vector3 position)
    {
        ParticleSystem.Particle r = new ParticleSystem.Particle();
        r.angularVelocity = 0;
        r.color = Color.white;
        r.startLifetime = Mathf.Infinity;
        r.position = position;
        r.rotation = UnityEngine.Random.Range(0f, 360f);
        r.size = UnityEngine.Random.Range(minSize, maxSize);
        r.velocity = Vector3.zero;

        return r;
    }
}
