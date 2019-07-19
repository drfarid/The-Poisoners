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
    Renderer swirlMesh;


    public void addItem(IInventoryItem newItem) {
    	if (newItem != null)
    		items.Add(newItem);
    		itemCounter++;
    		playerInventory.RemoveItem(newItem);
    }

    public int getCount() { return itemCounter; }

    public void mixItems() {

        bool speedPotion = false;
        bool strengthPotion = false;
        bool hallucinatePotion = false;
    	bool hasFlower = false;
    	bool hasRock = false;
        bool hasGem = false;

        bool hasBerry = false;
        bool hasShroom = false;
        bool hasHoney = false;

        if (SceneManager.GetActiveScene().name == "the_desert") {

	    	foreach (IInventoryItem item in items) {
	    		if (item.Name == "rock") {
	    			hasRock = true;
	    		}
	    		if (item.Name == "flower") {
	    			hasFlower = true;
	    		}
	    		if (item.Name == "crystal") {
	    			hasGem = true;
	    		}
	    	}
	    	
	    	if (hasFlower && hasRock && !hasGem) {
	    		speedPotion = true;
	    	} else if (hasFlower && hasRock && hasGem) {
	    		hallucinatePotion = true;
	    	}
	    	

	    	if (speedPotion) {
		    	swirl = GameObject.Find("bucket-swirl-disk");	
				swirlMesh = swirl.GetComponent<Renderer>();
				swirlMesh.enabled = true;
				swirl.transform.position = GameObject.Find("S_bucket").transform.position;
					
				foreach(IInventoryItem item in items) {
					item.gObj.SetActive(false);	
				}
				mixingCounter = 0;
				items = new List<IInventoryItem>();
				StartCoroutine(waitForSwirl("speed"));


			} else if (hallucinatePotion) {
				swirl = GameObject.Find("bucket-swirl-disk");	
				swirlMesh = swirl.GetComponent<Renderer>();
				swirlMesh.enabled = true;
				swirl.transform.position = GameObject.Find("S_bucket").transform.position;
					
				foreach(IInventoryItem item in items) {
					item.gObj.SetActive(false);	
				}
				mixingCounter = 0;
				items = new List<IInventoryItem>();
				StartCoroutine(waitForSwirl("hallucination"));
			}
		}
        else if (SceneManager.GetActiveScene().name == "forest")
        {

            foreach (IInventoryItem item in items)
            {
                if (item.Name == "berries")
                {
                    hasBerry = true;
                }
                else if (item.Name == "mushroom")
                {
                    hasShroom = true;
                }
                else if (item.Name == "honey")
                {
                    hasHoney = true;
                }
                else
                {
                    print("Unknown item: " + item.Name);
                }
            }

            if (hasBerry && hasShroom && !hasHoney)
            {
                strengthPotion = true;
            }
            else if (hasBerry && hasShroom && hasHoney)
            {
                hallucinatePotion = true;
            }


            if (strengthPotion)
            {
                swirl = GameObject.Find("bucket-swirl-disk");
                swirlMesh = swirl.GetComponent<Renderer>();
                swirlMesh.enabled = true;
                swirl.transform.position = GameObject.Find("S_bucket").transform.position;

                foreach (IInventoryItem item in items)
                {
                    item.gObj.SetActive(false);
                }
                mixingCounter = 0;
                items = new List<IInventoryItem>();
                StartCoroutine(waitForSwirl("strength"));


            }
            else if (hallucinatePotion)
            {
                swirl = GameObject.Find("bucket-swirl-disk");
                swirlMesh = swirl.GetComponent<Renderer>();
                swirlMesh.enabled = true;
                swirl.transform.position = GameObject.Find("S_bucket").transform.position;

                foreach (IInventoryItem item in items)
                {
                    item.gObj.SetActive(false);
                }
                mixingCounter = 0;
                items = new List<IInventoryItem>();
                StartCoroutine(waitForSwirl("hallucination"));
            }
        } else if (SceneManager.GetActiveScene().name == "win_scene") {
        	bool hasOne = false;
        	bool hasTwo = false;

        	foreach (IInventoryItem item in items)
            {
                if (item.Name == "rareOne")
                {
                    hasOne = true;
                }
                else if (item.Name == "rareTwo")
                {
                    hasTwo = true;
                }
            }

            if (hasOne && hasTwo) {
            	swirl = GameObject.Find("bucket-swirl-disk");
                swirlMesh = swirl.GetComponent<Renderer>();
                swirlMesh.enabled = true;
                swirl.transform.position = GameObject.Find("S_bucket").transform.position;

                foreach (IInventoryItem item in items)
                {
                    item.gObj.SetActive(false);
                }
                mixingCounter = 0;
                items = new List<IInventoryItem>();
                StartCoroutine(waitForSwirl("mith"));
            }
        }





    }
    public IEnumerator waitForSwirl(string potion) {
    	yield return new WaitForSeconds(1);
    	
    	swirlMesh.enabled = false;
    	

    	if (potion == "hallucination") {
	    	GameObject newPotion = Instantiate(GameObject.Find("HallucinatePotion"), new Vector3(0,0,0), Quaternion.identity);
	    	playerInventory.AddItem(newPotion.GetComponent<IInventoryItem>());
	    } else if (potion == "speed") {
	    	GameObject speedPotion = Instantiate(GameObject.Find("SpeedPotion"), new Vector3(0,0,0), Quaternion.identity);
	    	playerInventory.AddItem(speedPotion.GetComponent<IInventoryItem>());
        } else if (potion == "strength") {
            GameObject strengthPotion = Instantiate(GameObject.Find("StrengthPotion"), new Vector3(0, 0, 0), Quaternion.identity);
            playerInventory.AddItem(strengthPotion.GetComponent<IInventoryItem>());
        } else if (potion == "mith") {
      		GameObject mithPotion = Instantiate(GameObject.Find("MithPotion"), new Vector3(0,0,0), Quaternion.identity);
	    	playerInventory.AddItem(mithPotion.GetComponent<IInventoryItem>());  	
        }




CharacterInputController cic = GameObject.Find("Wizard Red").GetComponent<CharacterInputController>();
        if(cic.tutorialMode)
        {
            print("Switching scene");
            SceneManager.LoadScene("mixing_system");
        }

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
		            currentScale = new Vector3(currentScale.x * 4, currentScale.y *4, currentScale.z *4);
					itemObj.transform.localScale = currentScale;
		            break;     
		        case "flower-dried":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom":
		            currentScale = new Vector3(currentScale.x * 3, currentScale.y *3, currentScale.z *3);
					itemObj.transform.localScale = currentScale;
		            break;      
		        case "mushroom-dried":
		            currentScale = new Vector3(currentScale.x * 2, currentScale.y *2, currentScale.z *2);
					itemObj.transform.localScale = currentScale;
		            break;  
		        case "rareOne":
		        	currentScale = new Vector3(currentScale.x * 3, currentScale.y *3, currentScale.z *3);
					itemObj.transform.localScale = currentScale;
					break;
				 case "rareTwo":
		        	currentScale = new Vector3(currentScale.x * 3, currentScale.y *3, currentScale.z *3);
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
