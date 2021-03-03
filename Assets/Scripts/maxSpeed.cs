using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxSpeed : MonoBehaviour
{
    public float maxSpeedValue = 50f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeedValue)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeedValue;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
