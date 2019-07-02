using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class NpcController : MonoBehaviour
{
	public Animator NpcAnim;
    public GameObject[] waypoints;
    public int currWaypoint;
    public NavMeshAgent NpcNavmesh;
    public GameObject player;
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

		if (distance.magnitude > 1.5f) {
	           
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
	    	NpcAnim.SetFloat("vely",  0);
			NpcAnim.SetFloat("velx",  0);
			transform.LookAt(player3d);
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
				if (deathCount > 100) {
					SceneManager.LoadScene("main_menu", LoadSceneMode.Single);
				}
			}
	    }
        
	}



	
    
}
