using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInputController : MonoBehaviour {

    public string Name = "George P Burdell2";

    private float filteredForwardInput = 0f;
    private float filteredTurnInput = 0f;

    public bool InputMapToCircular = true;
    public float forwardInputFilter = 5f;
    public float turnInputFilter = 5f;

    public int delayCounter = 0;
    public Camera bucketCam;
    public Camera mainCam;
    private float forwardSpeedLimit = 1f;


    public Inventory inventory;
    public bool isMixing = false;
    public GameObject bucket;

    private CanvasGroup mixingCanvasGroup;

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
    	mixingCanvasGroup = GameObject.Find("Mixing_Canvas").GetComponent<CanvasGroup>();
        mixingCanvasGroup.interactable = false;
        mixingCanvasGroup.alpha = 0f;
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

	}


	private void OnTriggerEnter(Collider hit) {
        IInventoryItem item = hit.GetComponent<IInventoryItem>();
        if (item != null) {
            inventory.AddItem(item);
        }
    }
}
