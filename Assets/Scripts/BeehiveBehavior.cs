using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeehiveBehavior : MonoBehaviour
{
    private Animator anim;
    private GameObject bee1;
    private GameObject bee2;


    void Start()
    {
        anim = gameObject.GetComponentInParent<Animator>();
        bee1 = GameObject.Find("bee");
        bee2 = GameObject.Find("bee2");
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null) {
            if (c.gameObject.CompareTag("Player")) {
                anim.SetTrigger("ShowBees");
            }
        }
        
    }

    void OnTriggerStay(Collider c) {
        if (c.attachedRigidbody != null) {
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null) {
            if (c.gameObject.CompareTag("Player")) {
                anim.SetTrigger("HideBees");
            }
        }
    }

}
