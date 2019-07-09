using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour, IInventoryItem 
{
   
	public string Name {
		get {
			return "SpeedPotion";
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
		
	}
}
