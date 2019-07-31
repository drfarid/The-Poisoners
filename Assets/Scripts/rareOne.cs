using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rareOne : MonoBehaviour, IInventoryItem 
{
   
	public string Name {
		get {
			return "rareOne";
		}
	}

	public GameObject gObj {
		get {
			return gameObject;
		}
	}

	public Sprite _Image = null;

	public Sprite Image {
		get {
			return _Image;
		}
	}

	public void OnPickup() {
		gameObject.SetActive(false);
		if (SceneManager.GetActiveScene().name != "win_scene")
			SceneManager.LoadScene("the_desert", LoadSceneMode.Single);
        // GameObject wizard = GameObject.Find("Wizard_Red");
        // Animator anim = wizard.GetComponent<Animator>();
        // anim.SetBool("doButtonPress", true);
        // //anim.SetBool("doButtonPress", false);
        // print("pickup");

    }

    public void OnDrop() {
		
		
		GameObject bucket = GameObject.Find("S_bucket");
		BucketContainer bc = bucket.GetComponent<BucketContainer>();
		if (bucket.activeSelf) {
			gameObject.SetActive(true);
			Vector3 dropPoint = bucket.transform.position;
			dropPoint = new Vector3(dropPoint.x, dropPoint.y + 2f, dropPoint.z);
			gameObject.transform.position = dropPoint;
			
			Vector3 currentScale = transform.localScale;
			currentScale = new Vector3(currentScale.x / 3, currentScale.y/3, currentScale.z/3);
			gameObject.transform.localScale = currentScale;

			Collider c = gameObject.GetComponent<Collider>();
			c.enabled = true;
			c.isTrigger = false;
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			bc.addItem(this);
		}			

	}
}
