using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minPitch;
    public float maxPitch;

    private Rigidbody carRb;
    private AudioSource carAudio;

    private float currentSpeed;
    private float pitchFromCar;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude;
        pitchFromCar = carRb.velocity.magnitude / 50f;
        
        if (currentSpeed > minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }

    }
}
