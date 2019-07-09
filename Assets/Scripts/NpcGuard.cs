using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NpcGuard : MonoBehaviour
{
	public Animator NpcAnim;
    public NavMeshAgent NpcNavmesh;
    public GameObject player;
    public GameObject guardObject;

    Vector2 velocity;
    bool reachedPlayer;
    int attackCount;
    int deathCount = 0;

	

	void Start() {
		NpcNavmesh = GetComponent<NavMeshAgent>();
        NpcAnim = GetComponent<Animator>();
        reachedPlayer = false;
        NpcNavmesh.SetDestination(player.transform.position);
        attackCount = 0;
        deathCount = 0;
	}

	void Update() {
		Vector3 player3d = player.transform.position;
	    Vector3 agent3d = gameObject.transform.position;
	    Vector3 distance = player3d - agent3d;
	    float playerGuardDistance = (player.transform.position - guardObject.transform.position).magnitude;

	    float attackDistance;
	    float distanceFromGuard = (guardObject.transform.position - gameObject.transform.position).magnitude;

	    
	    if (playerGuardDistance < 25f) {
			if (distance.magnitude >= 2f) {
		           
		        Vector2 agentLocation = new Vector2(agent3d.x, agent3d.z);
		        Vector2 playerLocation = new Vector2(player3d.x, player3d.z);
		        Vector2 velocityUnit = (-playerLocation + agentLocation).normalized;
		        

		   
		    	NpcAnim.SetFloat("vely",  Mathf.Sqrt(velocityUnit.y * velocityUnit.y) * 1.5f);
				NpcAnim.SetFloat("velx",  velocityUnit.x);
		        
		        // NpcNavmesh.destination = player3d;
				transform.LookAt(player3d);
		        // // transform.position = player3d;
		        NpcNavmesh.SetDestination(player.transform.position);	
		        NpcAnim.SetBool("doButtonPress", false);

		    } else {
	            reachedPlayer= true;
		    	NpcAnim.SetFloat("vely",  0);
				NpcAnim.SetFloat("velx",  0);
	            NpcAnim.SetFloat("velz",  0);
				//transform.LookAt(player3d);
				NpcAnim.SetBool("doButtonPress", true);
				Slider playerHealth = (Slider) GameObject.Find("Slider").GetComponent<Slider>();
				
				attackCount++;
				if (attackCount > 20) {
					playerHealth.value -= 0.04f;
					attackCount = 0;
				}

				if (playerHealth.value == 0 || playerHealth.value < 0) {
					deathCount++;
					Animator playerAnim = player.GetComponent<Animator>();
					playerAnim.SetTrigger("isDead");
					if (deathCount > 200) {
						SceneManager.LoadScene("the_desert", LoadSceneMode.Single);
					}
				}
		    }
	    } else {	
	    	if (distanceFromGuard > 2f) {
		    	Vector2 agentLocation = new Vector2(agent3d.x, agent3d.z);
		        Vector2 guardLocation = new Vector2(guardObject.transform.position.x, guardObject.transform.position.z);
		        Vector2 velocityUnit = (-guardLocation + agentLocation).normalized;
		        
		    	NpcAnim.SetFloat("vely",  Mathf.Sqrt(velocityUnit.y * velocityUnit.y) * 1.5f);
				NpcAnim.SetFloat("velx",  velocityUnit.x);
		        
		        // NpcNavmesh.destination = player3d;
				transform.LookAt(guardObject.transform.position);
		        // // transform.position = player3d;
		        NpcNavmesh.SetDestination(guardObject.transform.position);
		    }
		    
	    }
        
	}



	
    
}
