using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class GreenBoar : MonoBehaviour
{
	public Animator NpcAnim;
    public NavMeshAgent NpcNavmesh;
    public GameObject player;
    public bool isInnerGuard;

    Vector2 velocity;
    bool reachedPlayer;
    int attackCount;
    int deathCount = 0;
    float healthPoints;
    int playerAttackCount;

	

	void Start() {
		NpcNavmesh = GetComponent<NavMeshAgent>();
        NpcAnim = GetComponent<Animator>();
        reachedPlayer = false;
        NpcNavmesh.SetDestination(player.transform.position);
        attackCount = 0;
        deathCount = 0;
        playerAttackCount = 0;
        healthPoints = 20f;
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log(other.gameObject.name);
		if (other.gameObject.name.Contains("PlayerProjectile")) {
			Debug.Log("got hit");
			healthPoints--;
			Slider bossHealth = (Slider)GameObject.Find("BossSlider").GetComponent<Slider>();
            bossHealth.value = (float)healthPoints / 20.0f;
            if (healthPoints == 0)
            {
                this.gameObject.SetActive(false);
                GameObject rareOne = GameObject.Find("rareOne");
                rareOne.transform.position = new Vector3(0, 2f, 0);
            }
		}
	}

	void Update() {
		Vector3 player3d = player.transform.position;
	    Vector3 agent3d = gameObject.transform.position;
	    Vector3 distance = player3d - agent3d;

	    float attackDistance;

        //if the player is more than 2 units away from the agent
        if (distance.magnitude >= 5f)
        {

            //get the velocity of the guard
            Vector2 agentLocation = new Vector2(agent3d.x, agent3d.z);
            Vector2 playerLocation = new Vector2(player3d.x, player3d.z);
            Vector2 velocityUnit = (-playerLocation + agentLocation).normalized;


            //set the velocity in x and y direction
            NpcAnim.SetFloat("vely", Mathf.Sqrt(velocityUnit.y * velocityUnit.y));
            NpcAnim.SetFloat("velx", velocityUnit.x);

            //look at the player and move towards him
            transform.LookAt(player3d);
            NpcNavmesh.SetDestination(player.transform.position);
            NpcAnim.SetBool("doButtonPress", false);

        }
        else
        {
            //reached the player, stop moving
            reachedPlayer = true;
            NpcAnim.SetFloat("vely", 0);
            NpcAnim.SetFloat("velx", 0);
            NpcAnim.SetFloat("velz", 0);

            float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;
            Animator playerAnim = player.GetComponent<Animator>();
            if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Combat") && distanceToPlayer <5f)
            {
                playerAttackCount++;
                if (playerAttackCount > 50)
                {
                    Debug.Log("Attacked boss");
                    healthPoints--;
                    playerAttackCount = 0;
                }
            }

            Slider bossHealth = (Slider)GameObject.Find("BossSlider").GetComponent<Slider>();
            bossHealth.value = (float)healthPoints / 20.0f;
            if (healthPoints == 0)
            {
                this.gameObject.SetActive(false);
                GameObject rareOne = GameObject.Find("rareOne");
                rareOne.transform.position = new Vector3(0, 2f, 0);
            }

            //start attacking and get the slider
            NpcAnim.SetBool("doButtonPress", true);
            Slider playerHealth = (Slider)GameObject.Find("Slider").GetComponent<Slider>();

            //attack the player every 20th frame
            attackCount++;
            if (attackCount > 20)
            {
                playerHealth.value -= 0.02f;
                attackCount = 0;
            }


            //if health of player is zero then restart the game
            if (playerHealth.value == 0 || playerHealth.value < 0)
            {
                deathCount++;

                playerAnim.SetTrigger("isDead");
                if (deathCount > 200)
                {
                    SceneManager.LoadScene("hallucination", LoadSceneMode.Single);
                }
            }
        }

	}


	
    
}
