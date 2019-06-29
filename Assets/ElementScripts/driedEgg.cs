using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driedEgg : MonoBehaviour, IInventoryItem 
{
   
	public string Name {
		get {
			return "egg-dried";
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
			currentScale = new Vector3(currentScale.x / 4, currentScale.y/4, currentScale.z/4);
			gameObject.transform.localScale = currentScale;

			Collider c = gameObject.GetComponent<Collider>();
			c.enabled = true;
			c.isTrigger = false;
			Rigidbody rb = gameObject.AddComponent<Rigidbody>();
			bc.addItem(this);
		}			

	}
}
