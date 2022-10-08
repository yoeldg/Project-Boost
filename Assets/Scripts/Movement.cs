using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustForce;
    [SerializeField] float rotationForce;
    Rigidbody myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("Rotate left");
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Rotate right");
            ApplyRotation(-rotationForce);

        }
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            Vector3 thrustVector = Vector3.up * thrustForce * Time.deltaTime;
            myRigidBody.AddRelativeForce(thrustVector);
        }  
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidBody.freezeRotation = true; //freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidBody.freezeRotation = false; //freeze rotation so physics system can take over
    }

}
