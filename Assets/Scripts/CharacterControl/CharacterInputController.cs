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
    public Camera mapViewCam;
    private float forwardSpeedLimit = 1f;
    public bool mapView = false;
    public GameObject playerMarker;


    public Inventory inventory;
    public bool isMixing = false;
    public GameObject bucket;

    private CanvasGroup potionTableGroup;
    private CanvasGroup mixingCanvasGroup;
    private Text tutorialText;
    private int tutorialStage =1;


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
        

        

        if (tutorialMode)
        {
            tutorialText = GameObject.Find("TutorialText").GetComponent<Text>();
        }
    }




    void Update () {
		
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
            forwardSpeedLimit = 0.1f;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            forwardSpeedLimit = 0.2f;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            forwardSpeedLimit = 0.3f;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            forwardSpeedLimit = 0.4f;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            forwardSpeedLimit = 0.5f;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            forwardSpeedLimit = 0.6f;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            forwardSpeedLimit = 0.7f;
        else if (Input.GetKeyUp(KeyCode.Alpha8))
            forwardSpeedLimit = 0.8f;
        else if (Input.GetKeyUp(KeyCode.Alpha9))
            forwardSpeedLimit = 0.9f;
        else if (Input.GetKeyUp(KeyCode.Alpha0))
            forwardSpeedLimit = 1.0f;

		
		


		delayCounter++;
        if (Input.GetKey(KeyCode.M)) {
            print("got mix");

            if (tutorialMode)
            {
                //tutorialStage = 7
                tutorialText.text = "Drag your items into the bucket one at a time. (click)";
                tutorialStage = 8;
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
	    } else if (Input.GetKey(KeyCode.Tab)) {
            print("got map");
            mapView = !mapView;

            if (mapView) {
                // overhead map view
                playerMarker.SetActive(true);
                mainCam.enabled = false;
                mapViewCam.enabled = true;
                Time.timeScale = 0f;
            } else {
                mapViewCam.enabled = false;
                mainCam.enabled = true;
                playerMarker.SetActive(false);
                Time.timeScale = 1f;
            }

        }
	    

        if (Input.GetKeyDown(KeyCode.Escape)) {
        	if (potionTableGroup.alpha == 0f) {
        		potionTableGroup.alpha = 1f; 	
        	} else {
        		potionTableGroup.alpha = 0f;
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
            tutorialText.text = "Good job!  You got him! (click)";
            tutorialStage = 6;

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
           // inventory.AddItem(currentItem);
        }

        if (tutorialMode == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (tutorialStage) 
                {
                    case 1: 
                        tutorialText.text = "Move to the mushroom and pick it up with the F key.";
                        tutorialStage = 2;
                        break;
                    case 4:
                        tutorialText.text = "Hit the Poisoner with Ctrl.  Don't let him steal! (click)";
                        tutorialNPC.SetActive(true);
                        tutorialStage = 5;
                        break;
                    case 6:
                        tutorialText.text = "Now you need to mix your poison.  Press M to mix.";
                        tutorialStage = 7;
                        break;
                    case 8:
                        tutorialText.text = "Press Mix when your bucket is full.  You are ready to start the game!";
                        tutorialStage = 7;
                        break;
                }
            }
        }
    }

    public void PickupItem()
    {
        inventory.AddItem(currentItem);

        if (tutorialMode == true)
        {
            //tutorialStage == 2
            if (currentItem.Name == "mushroom")
            {
                tutorialText.text = "Great!  Now pick up that berry.";
                tutorialStage = 3;
            }

            //tutorialStage == 3
            if (currentItem.Name == "berries")
            {
                tutorialText.text = "Awesome!  But watch out!  Another Poisoner is here. (click)";
                tutorialStage = 4;
            }

            print("pickup");
            print(currentItem.Name);
        }
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
