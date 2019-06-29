using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{

    private Animator anim;
    public float velx;
    public float vely;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        print(anim);
        //anim.StartPlayback();

    }
    // Start is called before the first frame update
    void Start()
    {
// thirdPersonCamera.desiredPose = wizard.transform.Find("CamPos");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // make coordinates circular
        //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
        //h = h * Mathf.Sqrt(1f - 0.5f * v * v);
        //v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        anim.SetFloat("velx", h);
        anim.SetFloat("vely", v);
        print(h.ToString());
        print(v.ToString());
        anim.speed = 1f;

    }
}
