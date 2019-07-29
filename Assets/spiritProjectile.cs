using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spiritProjectile : MonoBehaviour
{

	public GameObject player;
	int attackCount;
	int deathCount;
	Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        attackCount = 0;
        deathCount = 0;
        playerAnim = player.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
    	Slider playerHealth = (Slider) GameObject.Find("Slider").GetComponent<Slider>();
    	float distanceToPlayer = (player.transform.position - this.transform.position).magnitude;
        if (distanceToPlayer < 1f) {
        	
        	if (this.transform.position.y > 1.5f)
                //change it back to 0.01f
				playerHealth.value -= 0.01f;
				
			
        }

        if (playerHealth.value == 0 || playerHealth.value < 0) {
			deathCount++;
			
			playerAnim.SetTrigger("isDead");
			if (deathCount > 200) {
                LastDeathInformation.StageName = "hallucination2";
				SceneManager.LoadScene("death_scene", LoadSceneMode.Single);
			}
		}
    }
}
