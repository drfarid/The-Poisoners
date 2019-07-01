using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public float thrust;
    bool clicked = false;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        if ((rb.position.y <= 1.1) &&
            (Random.value > 0.99))// && 
            //(Random.value > 0.99))
        { 
            rb.AddForce(transform.up * thrust, ForceMode.VelocityChange);
            //clicked = true;
        }

    }

}
