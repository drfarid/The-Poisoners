using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class GiantTree : MonoBehaviour
{
    public Animator anim;
    public Animator playerAnim;
    public GameObject player;
    public GameObject treeObject;
    RootMotionControlScript rMotion;

    Vector2 velocity;
    bool reachedPlayer;
    int attackCount;
    int deathCount = 0;
    int healthPoints = 2;
    int playerAttackCount;

	

	void Start() {
        anim = GetComponent<Animator>();
        rMotion = player.GetComponent<RootMotionControlScript>();
        reachedPlayer = false;
        attackCount = 0;
        deathCount = 0;
        playerAttackCount = 0;
        healthPoints = 2;
	}

	void Update() {
		Vector3 player3d = player.transform.position;
	    Vector3 tree3d = treeObject.transform.position;
	    Vector3 distance = player3d - tree3d;
        	    
        //player is in range and is attacking - knock the tree down
		Animator playerAnim = player.GetComponent<Animator>();
		if (distance.magnitude < 10f &&
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Combat")) {
    		Debug.Log("player attacked tree");
            if (rMotion.isInStrengthBoost)
            {
                print("knocking down tree");
                anim.SetTrigger("fall");
                //StartCoroutine(rMotion.growWizard(false));
            }
            else
            {
                print("attacked tree but no strength boost");
            }
        }

    }
}
