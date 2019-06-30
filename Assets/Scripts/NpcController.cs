using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

	

	void Start() {
		NpcNavmesh = GetComponent<NavMeshAgent>();
        NpcAnim = GetComponent<Animator>();
        reachedPlayer = false;
        NpcNavmesh.SetDestination(player.transform.position);
	}

	void Update() {
		Vector3 player3d = player.transform.position;
	    Vector3 agent3d = gameObject.transform.position;
	    Vector3 distance = player3d - agent3d;

		if (distance.magnitude > 1f) {
	           
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
	    }
        
	}



	
    
}
