using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterInputController : MonoBehaviour {

    public string Name = "George P Burdell2";

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;
    public bool InputMapToCircular = true;
    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;
    public bool tutorialMode = false;
    public GameObject tutorialNPC;



    IInventoryItem currentItem;

    public int delayCounter = 0;
    public Camera bucketCam;
    public Camera mainCam;
    private float forwardSpeedLimit = 0.5f;
    //public bool mapView = false;
    public GameObject playerMarker;


    public Inventory inventory;
    public bool isMixing = false;
    public GameObject bucket;

    private CanvasGroup potionTableGroup;
    private CanvasGroup mixingCanvasGroup;
    public int tutorialStage =1;


    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public bool Action
    {
        get;
        private set;
    }

    void Start() {
        

        if (SceneManager.GetActiveScene().name == "win_scene") {
            CanvasGroup winCanvas = GameObject.Find("Winning_Canvas").GetComponent<CanvasGroup>();
            winCanvas.interactable = false;
            winCanvas.alpha = 0f;
        }

    	mixingCanvasGroup = GameObject.Find("Mixing_Canvas").GetComponent<CanvasGroup>();
        mixingCanvasGroup.interactable = false;
        potionTableGroup = GameObject.Find("Potion_Table_Canvas").GetComponent<CanvasGroup>();
        potionTableGroup.interactable = false;
     	potionTableGroup.alpha = 0f;
        mixingCanvasGroup.alpha = 0f;

        if (SceneManager.GetActiveScene().name == "the_desert")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("This is the desert.  You'll need to find desert rose and pigment rocks, Press R to throw fireballs!");
        }

        if (SceneManager.GetActiveScene().name == "forest")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("Welcome to the forest.\nFirst look for mushrooms in the shade of the mountain.");
        }

        if (SceneManager.GetActiveScene().name == "tutorial")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("Welcome to the tutorial.  I'll teach you how to play the game. (click)");
        }

        if (SceneManager.GetActiveScene().name == "hallucination")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("Destroy the green boar! You'll be rewarded with a gem which will take you back to the real world.");
        }

        if (SceneManager.GetActiveScene().name == "hallucination2")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("Destroy the demon to get another gem! He teleports so you'll need to be quick!");
        }

        if (SceneManager.GetActiveScene().name == "win_scene")
        {
            EventManager.TriggerEvent<SpeakEvent, string>("You've made it to the pinnacle.  Mix the rare gems to create the ultimate antidote!");
        }



    }




    void Update () {

        string crowText = null;

        //GetAxisRaw() so we can do filtering here instead of the InputManager
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis


        if (InputMapToCircular)
        {
            // make coordinates circular
            //based on http://mathproofs.blogspot.com/2005/07/mapping-square-to-circle.html
            h = h * Mathf.Sqrt(1f - 0.5f * v * v);
            v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        }


        //BEGIN ANALOG ON KEYBOARD DEMO CODE
        if (Input.GetKey(KeyCode.Q))
            h = -0.5f;
        else if (Input.GetKey(KeyCode.E))
            h = 0.5f;
           
                   
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("forest");
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("hallucination");
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("the_desert");
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("hallucination2");
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("win_scene");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            forwardSpeedLimit = Mathf.Lerp(forwardSpeedLimit, 0.5f, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            forwardSpeedLimit = Mathf.Lerp(forwardSpeedLimit, 1.0f, 0.5f);
        }


        delayCounter++;
        if (Input.GetKey(KeyCode.M)) {

            if (tutorialMode)
            {
                if (tutorialStage == 7)
                {
                    crowText = "Drag your items into the bucket one at a time. (click)";
                    tutorialStage = 8;
                }
                else if (tutorialStage == 10)
                {
                    //tutorialStage11
                    crowText = "Now ingest the poison with P to build your immunity.  The game will start.";
                    tutorialStage = 11;

                }
            }


            if (delayCounter > 10) {
        		delayCounter = 0;
	        	if (bucket.activeSelf == true) {
	        		bucketCam.enabled = false;
	        		mainCam.enabled = true;
	        		bucket.SetActive(false);	
	        		
                    mixingCanvasGroup.interactable = false;
                    mixingCanvasGroup.alpha = 0f;                   

	        	} else {
	        		if (bucket == null)
	        			bucket = Instantiate(bucket, this.transform.position, Quaternion.identity);
	        		Vector3 bucketPosition = this.transform.position;

	        		bucketPosition = new Vector3(bucketPosition.x, bucketPosition.y + 1f, bucketPosition.z + 1f);
	        		bucket.transform.position = bucketPosition;

	        		bucket.SetActive(true);
	        		bucketCam.enabled = true;
	        		mainCam.enabled = false;

                    mixingCanvasGroup.interactable = true;
                    mixingCanvasGroup.alpha = 1f;
	        		
	        	}
        	}
	    } //else if (Input.GetKey(KeyCode.Tab)) {
     //        print("got map");
     //        mapView = !mapView;

     //        if (mapView) {
     //            // overhead map view
     //            playerMarker.SetActive(true);
     //            mainCam.enabled = false;
     //            mapViewCam.enabled = true;
     //            Time.timeScale = 0f;
     //        } else {
     //            mapViewCam.enabled = false;
     //            mainCam.enabled = true;
     //            playerMarker.SetActive(false);
     //            Time.timeScale = 1f;
     //        }

     //    }
	    

        if (Input.GetKeyDown(KeyCode.Escape)) {
        	if (potionTableGroup.alpha == 0f) {
        		potionTableGroup.alpha = 1f;
                if (tutorialMode)
                {
                    crowText = "These are the potions you can make.  You will mix a Strength potion.  Hit Esc to dismiss recipes.";
                }
            }
            else {
        		potionTableGroup.alpha = 0f;
                if (tutorialMode)
                {
                    crowText = "Now hit M to mix your Strength potion.";
                    tutorialStage = 7;
                }
            }

        }

        //do some filtering of our input as well as clamp to a speed limit
        filteredForwardInput = Mathf.Clamp(Mathf.Lerp(filteredForwardInput, v, 
            Time.deltaTime * forwardInputFilter), -forwardSpeedLimit, forwardSpeedLimit);

        filteredTurnInput = Mathf.Lerp(filteredTurnInput, h, 
            Time.deltaTime * turnInputFilter);

        Forward = filteredForwardInput;
        Turn = filteredTurnInput;


        //Capture "fire" button for action event
        Action = Input.GetButtonDown("Fire1");

        if (tutorialMode && Action)
        {
            //tutorialStage = 5
            tutorialNPC.SetActive(false);
            crowText = "Good job!  You got him! (click)";
            tutorialStage = 6;

        }

        if (tutorialMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (tutorialStage) 
                {
                    case 1:
                        crowText = "Move to the mushroom and pick it up with the F key.";
                        tutorialStage = 2;
                        break;
                    case 4:
                        crowText = "Hit the Poisoner with Ctrl.  Don't let him steal! (click)";
                        tutorialNPC.SetActive(true);
                        tutorialStage = 5;
                        break;
                    case 6:
                        crowText = "Now you need to mix your poison.  Press Esc to view recipes.";
                        tutorialStage = 7;
                        break;
                    case 9:
                        crowText = "Press Mix when your bucket is full.";
                        tutorialStage = 10;
                        break;
                }
            }
        }

        if (crowText != null)
        {
            EventManager.TriggerEvent<SpeakEvent, string>(crowText);
        }

    }

    public void PickupItem()
    {
        string crowText = null;

        if (SceneManager.GetActiveScene().name == "forest")
        {
            if (currentItem.Name == "mushroom")
            {
                if (!inventory.ContainsItem("mushroom"))
                {
                    crowText = "Good find!  You'll need one more mushroom";
                }
                else
                {
                    if (!inventory.ContainsItem("berries"))
                    {
                        crowText = "OK, now you need to find berries.  They grow out by the water to your left.";
                    }

                }
            }

            if (currentItem.Name == "berries")
            {
                if (!inventory.ContainsItem("berries"))
                {
                    crowText = "That's a ripe one!  You'll need one more.";
                }
                else
                {
                    if (inventory.ContainsItem("mushroom"))
                    {
                        crowText = "Next you'll need honey which grows in a great tree.\nMix a strength potion (Tab key for recipe).";
                    }

                }
            }

        }

        if (SceneManager.GetActiveScene().name == "the_desert")
        {
            if (currentItem.Name == "flower")
            {
                if (!inventory.ContainsItem("flower"))
                {
                    crowText = "That's a pretty one.  You'll need one more rose.";
                }
                else
                {
                    if (inventory.ContainsItem("rock"))
                    {
                        crowText = "Look for a flat spot in the sand and press Space to dig.\nMake sure to drink the Speed potion.";
                    }
                    else
                    {
                        crowText = "Very good. You'll need two rocks and two desert roses.";
                    }


                }
            }

            if (currentItem.Name == "rock")
            {
                if (!inventory.ContainsItem("rock"))
                {
                    crowText = "Very good.  Try to find one more.";
                }
                else
                {
                    if (inventory.ContainsItem("flower"))
                    {
                        crowText = "Look for a flat spot in the sand and press Space to dig.\nDrink the Speed potion before you dig.";
                    }
                    else
                    {
                        crowText = "Very good. You'll need two rocks and two desert roses.";
                    }

                }
            }

            if (currentItem.Name == "gem")
            {
                if (inventory.ContainsItem("rock") && inventory.ContainsItem("rock"))
                {
                    crowText = "A beautiful stone.  Mix it with a rose and pigment stone to create a poison for the dream world.";
                }
                else
                {
                    crowText = "A beautiful stone.  You'll also need a rose and pigment stone to formulate the poison.";
                }
            }

        }

        if (SceneManager.GetActiveScene().name == "tutorial")
        {
            //tutorialStage == 2
            if (currentItem.Name == "mushroom")
            {
                crowText = "Great!  Now pick up that berry.";
                tutorialStage = 3;
            }

            //tutorialStage == 3
            if (currentItem.Name == "berries")
            {
                crowText = "Awesome!  But watch out!  Another Poisoner is here. (click)";
                tutorialStage = 4;
            }

        }

        if (SceneManager.GetActiveScene().name == "death_scene") {
            if (currentItem.Name == "skull") {
                Debug.Log("got skull");
                Application.Quit();
            } 
            if (currentItem.Name == "flower") {
                if (LastDeathInformation.StageName != null) {
                    SceneManager.LoadScene(LastDeathInformation.StageName);
                } else {
                    SceneManager.LoadScene("forest");
                }
            }
        }

        EventManager.TriggerEvent<SpeakEvent, string>(crowText);

        inventory.AddItem(currentItem);


    }

    private void OnTriggerEnter(Collider hit) {
        IInventoryItem item = hit.GetComponent<IInventoryItem>();
        if (item != null) {
            currentItem = item;
            // inventory.AddItem(item);
        }
    }

    private void OnTriggerExit(Collider hit)
    {
            currentItem = null;
    }
}
