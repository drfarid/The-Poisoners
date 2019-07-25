using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTwo : MonoBehaviour
{
	public GameObject projectile;
	public GameObject player;
	Animator playerAnim;
	bool justTeleported;
	bool justAttacked;
	int playerAttackCount;
	int healthPoints = 20;
    // Start is called before the first frame update
    void Start()
    {
        justTeleported = false;
        justAttacked = false;
        playerAttackCount = 0;
        playerAnim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    	transform.LookAt(player.transform.position);
     	if (!justTeleported) {
     		teleportRandom();
     		justTeleported = true;
     	} 

     	if (!justAttacked) {
     		attack();
     		justAttacked = true;
     	}  

     	float distanceToPlayer = (this.transform.position - player.transform.position).magnitude;
     	if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Combat") && distanceToPlayer < 4f) {
    		playerAttackCount++;
    		if (playerAttackCount > 50) {
    			Debug.Log("Attacked boss");
    			healthPoints--;
    			playerAttackCount = 0;	
    		}
    	}

    	Slider bossHealth = (Slider) GameObject.Find("BossSlider").GetComponent<Slider>();
    	bossHealth.value = (float) healthPoints/20.0f;
    	if (healthPoints == 0) {
    		this.gameObject.SetActive(false);
            GameObject rareTwo = GameObject.Find("rareTwo");
            rareTwo.transform.position = new Vector3(0, 2f, 0);
    	}
    }

    void attack() {
    	
    	Vector3 startPoint = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
    	GameObject projectileOne = Instantiate(projectile,startPoint, Quaternion.identity);
		Rigidbody pOne = projectileOne.GetComponent<Rigidbody>();
		Vector3 forceDirection = Vector3.Normalize(player.transform.position - startPoint);
		pOne.AddForce(forceDirection.x * 15f, forceDirection.y * 15f + 4f, forceDirection.z * 15f, ForceMode.Impulse);
		float time = Random.Range(1f, 3f);
		StartCoroutine(delayAttack(time, projectileOne));

    }

    public IEnumerator delayAttack(float time, GameObject oldProjectile) {
        yield return new WaitForSeconds(time);
        justAttacked = false;
        oldProjectile.SetActive(false);

    }

    void teleportRandom() {
    	float x = Random.Range(-10f, 10f);
    	float z = Random.Range(-10f, 10f);
    	float time = Random.Range(2f, 7f);
    	this.transform.position = new Vector3(x, this.transform.position.y, z);
    	StartCoroutine(stay(time));

    }

    public IEnumerator stay(float time) {
        yield return new WaitForSeconds(time);
        justTeleported = false;

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
}
