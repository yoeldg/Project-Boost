using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 degreesPerSecond;

    void Update()
    {
        transform.localEulerAngles += degreesPerSecond * Time.deltaTime;
        Debug.Log(degreesPerSecond * Time.deltaTime);
    }
}
