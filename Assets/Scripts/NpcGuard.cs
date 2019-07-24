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
    public bool isInnerGuard;

    Vector2 velocity;
    bool reachedPlayer;
    int attackCount;
    int deathCount = 0;
    int healthPoints = 2;
    int playerAttackCount;

	

	void Start() {
		NpcNavmesh = GetComponent<NavMeshAgent>();
        NpcAnim = GetComponent<Animator>();
        reachedPlayer = false;
        NpcNavmesh.destination = player.transform.position;
        attackCount = 0;
        deathCount = 0;
        playerAttackCount = 0;
        healthPoints = 2;
	}

	void Update() {
		Vector3 player3d = player.transform.position;
	    Vector3 agent3d = gameObject.transform.position;
	    Vector3 distance = player3d - agent3d;
	    float playerGuardDistance = (player.transform.position - guardObject.transform.position).magnitude;

	    float attackDistance;
	    float distanceFromGuard = (guardObject.transform.position - gameObject.transform.position).magnitude;

	    float chaseThreshold;
	    if (isInnerGuard) {
	    	chaseThreshold = 20f;
	    } else {
	    	chaseThreshold = 35f;
	    }
	    
	    //if player is less than 25 units away from the guarded object
	    if (playerGuardDistance < chaseThreshold) {

	    	//if the player is more than 2 units away from the agent
			if (distance.magnitude >= 2f) {
		           
		        //get the velocity of the guard
		        Vector2 agentLocation = new Vector2(agent3d.x, agent3d.z);
		        Vector2 playerLocation = new Vector2(player3d.x, player3d.z);
		        Vector2 velocityUnit = (-playerLocation + agentLocation).normalized;
		        

		   		//set the velocity in x and y direction
		    	NpcAnim.SetFloat("vely",  Mathf.Sqrt(velocityUnit.y * velocityUnit.y));
				NpcAnim.SetFloat("velx",  velocityUnit.x);
		        
		       	//look at the player and move towards him
				transform.LookAt(player3d);
		        NpcNavmesh.destination = player.transform.position;	
		        NpcAnim.SetBool("doButtonPress", false);

		    } else {
	            //player is in range and is attacking
		    	Animator playerAnim = player.GetComponent<Animator>();
		    	if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Combat")) {
		    		Debug.Log("player attacked");
		    		playerAttackCount++;
		    		if (playerAttackCount > 50) {
		    			healthPoints--;
		    			playerAttackCount = 0;	
		    		}
		    	}

		    	if (healthPoints == 0 || healthPoints < 0) {
    				this.gameObject.SetActive(false);
    			}

		    	//reached the player, stop moving
	            reachedPlayer= true;
		    	NpcAnim.SetFloat("vely",  0);
				NpcAnim.SetFloat("velx",  0);
	            
				
				//start attacking and get the slider
				NpcAnim.SetBool("doButtonPress", true);
				Slider playerHealth = (Slider) GameObject.Find("Slider").GetComponent<Slider>();
				
				//attack the player every 20th frame
				attackCount++;
				if (attackCount > 20) {
					playerHealth.value -= 0.02f;
					attackCount = 0;
				}

				//if health of player is zero then restart the game
				if (playerHealth.value == 0 || playerHealth.value < 0) {
					deathCount++;
					
					playerAnim.SetTrigger("isDead");
					if (deathCount > 200) {
						SceneManager.LoadScene("the_desert", LoadSceneMode.Single);
					}
				}
		    }
	    } else {	
	    	float guardDistance = 2f;
	    	if (guardObject.name == "gem2") {
	    		guardDistance = 15f;
	    	}

	    	if (distanceFromGuard > guardDistance) {
		    	Vector2 agentLocation = new Vector2(agent3d.x, agent3d.z);
		        Vector2 guardLocation = new Vector2(guardObject.transform.position.x, guardObject.transform.position.z);
		        Vector2 velocityUnit = (-guardLocation + agentLocation).normalized;
		        
		    	NpcAnim.SetFloat("vely",  Mathf.Sqrt(velocityUnit.y * velocityUnit.y) * 1.5f);
				NpcAnim.SetFloat("velx",  velocityUnit.x);
		        
		        // NpcNavmesh.destination = player3d;
				transform.LookAt(guardObject.transform.position);
		        // // transform.position = player3d;
		        NpcNavmesh.destination = guardObject.transform.position;
		    } else {
		    	NpcAnim.SetFloat("vely",  0);
				NpcAnim.SetFloat("velx",  0);
	            
		    }
		    
	    }
        
	}


	
    
}
