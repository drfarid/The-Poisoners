using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomDried : MonoBehaviour, IInventoryItem 
{
   
	public string Name {
		get {
			return "mushroomDried";
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
}
