using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebush : MonoBehaviour
{
    ParticleSystem ps;

    private void Awake()
    {
        ps = this.GetComponentInChildren<ParticleSystem>();
        //ps = GameObject.Find("FirebushParticle").GetComponent<ParticleSystem>();
        ps.Stop();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                ps.Play();
            }
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                ps.Stop();
            }
        }
    }
}
