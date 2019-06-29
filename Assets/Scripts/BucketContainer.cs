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

    public void addItem(IInventoryItem newItem) {
    	if (newItem != null)
    		items.Add(newItem);
    		itemCounter++;
    }

    public int getCount() { return itemCounter; }

    public void mixItems() {
    	SceneManager.LoadScene("game_alpha1", LoadSceneMode.Single);
    }
    
    public void cancelMix() {
    	foreach (IInventoryItem item in items) {
    		if (item != null) {
    			GameObject itemObj = item.gObj;


				Vector3 currentScale = itemObj.transform.localScale;
				
				switch (item.Name)
		        {
		        case "rock":
		            currentScale = new Vector3(currentScale.x * 10, currentScale.y *10, currentScale.z *10);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "egg":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y * 4, currentScale.z * 4);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "berries":
		            currentScale = new Vector3(currentScale.x * 6, currentScale.y *6, currentScale.z *6);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "bushes":
		            currentScale = new Vector3(currentScale.x * 20, currentScale.y *20, currentScale.z *20);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "crystal":
		            currentScale = new Vector3(currentScale.x * 5, currentScale.y *5, currentScale.z *5);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "egg-dried":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;
		        case "flower":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;     
		        case "flower-dried":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom-dried":
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;      
		        default:
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;
		        }
	    		playerInventory.AddItem(item);
	    	

	    	}
    	}
    	itemCounter = 0;
    }

}
