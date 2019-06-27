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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // make coordinates circular
        //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
        h = h * Mathf.Sqrt(1f - 0.5f * v * v);
        v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        anim.SetFloat("velx", v);
        anim.SetFloat("vely", h);
        print(velx.ToString());
        print(vely.ToString());
        anim.speed = 1f;

    }
}
