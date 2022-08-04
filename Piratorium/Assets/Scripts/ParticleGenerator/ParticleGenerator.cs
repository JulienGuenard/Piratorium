using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public GameObject quad;

    public float particleCadence;
    [Range(0, 360)]public float particleDirection;
    public float particleSpeedStart;
    public float particleSlow;
    public float particleDestroyTime;
    public float particleCircleMultiplier;

    float cadenceNextTime;
    Vector2 randomCircle;

    private void Awake()
    {
        cadenceNextTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= cadenceNextTime)
        {
            cadenceNextTime = Time.time + particleCadence;
            CreateQuad();
        }
    }

    void CreateQuad()
    {
        randomCircle = Random.insideUnitCircle;

        Vector2 pos = transform.position;

        pos += randomCircle * particleCircleMultiplier;

        GameObject obj = Instantiate(quad, pos, Quaternion.identity);

        obj.transform.RotateAround(transform.position, transform.forward, particleDirection);
        Quad objQuad = obj.GetComponent<Quad>();
        objQuad.SetDestroyTime(particleDestroyTime);
        objQuad.SetSpeed(particleSpeedStart);
        objQuad.SetSlow(particleSlow);
    }
}
