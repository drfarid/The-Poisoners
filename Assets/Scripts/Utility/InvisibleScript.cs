﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
