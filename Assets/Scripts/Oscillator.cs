using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2; // ~ 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  // -1 to 1
        movementFactor = (rawSinWave + 1f) / 2f; // 0 to 1

        Vector3 offset = movementVector * movementFactor; 
        transform.position = startPosition + offset;
    }
}
