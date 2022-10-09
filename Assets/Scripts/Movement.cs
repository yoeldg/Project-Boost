using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce;
    [SerializeField] float rotationForce;
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;


    Rigidbody myRigidBody;
    AudioSource audioSource;
    
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }
    void ProcessRotation()
    { 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotate();
        }
    }

    private void StopRotate()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void RotateRight()
    {
        Debug.Log("Rotate right");
        ApplyRotation(-rotationForce);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        Debug.Log("Rotate left");
        ApplyRotation(rotationForce);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    

    private void StopThrust()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    private void StartThrust()
    {
        Vector3 thrustVector = Vector3.up * thrustForce * Time.deltaTime;
        myRigidBody.AddRelativeForce(thrustVector);
        if (!audioSource.isPlaying)
        {
            audioSource.clip = mainEngineAudio;
            audioSource.Play();
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidBody.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidBody.freezeRotation = false; //freeze rotation so physics system can take over
    }
}
