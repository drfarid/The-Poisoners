using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class BucketContainer : MonoBehaviour
{
	public Inventory playerInventory;
    List<IInventoryItem> items = new List<IInventoryItem>();
    int itemCounter = 0;
    int mixingCounter = 0;
    GameObject swirl;
    

    public void addItem(IInventoryItem newItem) {
    	if (newItem != null)
    		items.Add(newItem);
    		itemCounter++;
    		playerInventory.RemoveItem(newItem);
    }

    public int getCount() { return itemCounter; }

    public void mixItems() {
    	
    	if (items.Count > 1) {
	    	swirl = GameObject.Find("bucket-swirl-disk");
	    	swirl.transform.position = GameObject.Find("S_bucket").transform.position;

			foreach(IInventoryItem item in items) {
				Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
				
				collider.enabled = false;
				
			}
			mixingCounter = 0;
			StartCoroutine(waitForSwirl());
			
		}

    	
    }
    public IEnumerator waitForSwirl() {
    	yield return new WaitForSeconds(1);
    	swirl.SetActive(false);
    	playerInventory.AddItem(GameObject.Find("HallucinatePotion").GetComponent<IInventoryItem>());
    }
    
    public void cancelMix() {

    	itemCounter = 0;
    	foreach (IInventoryItem item in items) {

    		if (item != null) {
    			GameObject itemObj = item.gObj;


				Vector3 currentScale = itemObj.transform.localScale;
				
				switch (item.Name)
		        {
		        case "rock":
		            currentScale = new Vector3(currentScale.x * 3, currentScale.y *3, currentScale.z *3);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "egg":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y * 2, currentScale.z * 2);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "berries":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "bushes":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "crystal":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "egg-dried":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "flower":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;     
		        case "flower-dried":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom-dried":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;      
		        default:
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;
		        }
	    		playerInventory.AddItem(item);

	    	

	    	}
    	}
    	// foreach (IInventoryItem removeItem in items) {
    	// 	items.Remove(removeItem);
    	// }

    	items = new List<IInventoryItem>();
    	itemCounter = 0;
    }

}
