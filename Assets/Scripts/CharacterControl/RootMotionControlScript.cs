using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

//require some things the bot control needs
[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterInputController))]
public class RootMotionControlScript : MonoBehaviour
{
    private Animator anim;	
    private Rigidbody rbody;
    private CharacterInputController cinput;
    public Inventory playerInventory;
    private Transform leftFoot;
    private Transform rightFoot;

    //Useful if you implement jump in the future...
    public float jumpableGroundNormalMaxAngle = 45f;
    public bool closeToJumpableGround;

    private int groundContactCount = 0;

    // Animation speed adjustments
    public float animationSpeed = 1f;
    public float rootMovementSpeed = 1f;
    public float rootTurnSpeed = 1f;

    GameObject shovel;
    int speedBoostDuration = 1000;
    public bool isInSpeedBoost = false;
    public bool isInStrengthBoost = false;
    int speedMultiplier = 1;
    CrowBehavior crow;

    public GameObject buttonObject;

    public bool IsGrounded
    {
        get
        {
            return groundContactCount > 0;
        }
    }

    public IEnumerator waitForMessage(int time) {
        yield return new WaitForSeconds(time);
    }

    void Awake()
    {


        anim = GetComponent<Animator>();

        if (anim == null)
            Debug.Log("Animator could not be found");

        rbody = GetComponent<Rigidbody>();

        if (rbody == null)
            Debug.Log("Rigid body could not be found");

        cinput = GetComponent<CharacterInputController>();
        if (cinput == null)
            Debug.Log("CharacterInput could not be found");
    }


    // Use this for initialization
    void Start()
    {
		//example of how to get access to certain limbs
        leftFoot = this.transform.Find("mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot");
        rightFoot = this.transform.Find("mixamorig:Hips/mixamorig:RightUpLeg/mixamorig:RightLeg/mixamorig:RightFoot");

        if (leftFoot == null || rightFoot == null)
            Debug.Log("One of the feet could not be found");


        
    	if (SceneManager.GetActiveScene().name == "win_scene") {
            GameObject rareOne = GameObject.Find("rareOne");
            GameObject rareTwo = GameObject.Find("rareTwo");
            playerInventory.AddItem(rareOne.GetComponent<IInventoryItem>());
            playerInventory.AddItem(rareTwo.GetComponent<IInventoryItem>());
        }
            
    }
        





    void Update()
    {


        
        if (isInSpeedBoost) {  
            speedMultiplier = 2;
            speedBoostDuration--;
            if (speedBoostDuration == 0) {
                isInSpeedBoost = false;
                speedBoostDuration = 1000;
                speedMultiplier = 1;
            }
        }

        float inputForward=0f;
        float inputTurn=0f;
        bool inputAction = false;
        // input is polled in the Update() step, not FixedUpdate()
        // Therefore, you should ONLY use input state that is NOT event-based in FixedUpdate()
        // Input events should be handled in Update(), and possibly passed on to FixedUpdate() through 
        // the state of the MonoBehavior
        if (cinput.enabled)
        {
            inputForward = cinput.Forward;
            inputTurn = cinput.Turn;
            inputAction = cinput.Action;
                
        }

        //onCollisionXXX() doesn't always work for checking if the character is grounded from a playability perspective
        //Uneven terrain can cause the player to become technically airborne, but so close the player thinks they're touching ground.
        //Therefore, an additional raycast approach is used to check for close ground
        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);
                                                    
       
        anim.SetFloat("velx", inputTurn);	
        anim.SetFloat("vely", inputForward);
        anim.SetBool("isFalling", !isGrounded);
        anim.SetBool("doButtonPress", inputAction);
        
        
        anim.speed = animationSpeed * speedMultiplier;

        if (Input.GetKeyDown(KeyCode.B))
        {
            // testing only
            EventManager.TriggerEvent<SpeakEvent, string>("testing event system");
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("isPickup");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            anim.SetTrigger("isHit");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("isDead");
        }

        if (Input.GetKeyDown(KeyCode.R)) {
        	if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Throw")) {
        		anim.SetTrigger("isThrowing");
        		StartCoroutine(waitForThrow());	
        	}
        	
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            
            if (SceneManager.GetActiveScene().name == "the_desert") {
            	if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Digging")) {
		            anim.SetTrigger("isDigging");
		            shovel = GameObject.Find("Shovel_Holder").transform.GetChild(0).gameObject;
		            shovel.SetActive(true);
		            StartCoroutine(waitForDig());
	          	}
        	} else if (SceneManager.GetActiveScene().name == "win_scene") {
        		Rigidbody playerBody = this.gameObject.GetComponent<Rigidbody>();
        		playerBody.AddForce(0, 10f, 0, ForceMode.VelocityChange);
        	}
		}
        if (Input.GetKeyDown(KeyCode.P))
        {
            print("P button pressed.");
            Transform inventoryPanel = GameObject.Find("Inventory").GetComponent<Transform>();
            
            foreach (Transform findPotion in inventoryPanel) {
                
                Transform imageTransform = findPotion.GetChild(0).GetChild(0);
                Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                

                IInventoryItem itemObject = itemDragHandler.Item;
                // print(itemObject.Name);
                
                if (itemObject != null) {
                    Debug.Log("item: " + itemObject.Name);
                    if (itemObject.Name == "HallucinationPotion") {
                        print("Drinking hallucination potion");
                        Slider playerHealth = (Slider) GameObject.Find("Slider").GetComponent<Slider>();
                        playerHealth.value += 0.2f;
                        image.enabled = false;
                        image.sprite = null;
                        itemDragHandler.Item = null;
                        anim.SetTrigger("isDrinking");
                        StartCoroutine(waitForDrink("hallucination"));
                        break;
                    } else if (itemObject.Name == "SpeedPotion") {
                        print("Drinking speed potion");
                        image.enabled = false;
                        image.sprite = null;
                        itemDragHandler.Item = null;
                        anim.SetTrigger("isDrinking");
                        StartCoroutine(waitForDrink("speed"));
                        break;
                    }
                    else if (itemObject.Name == "StrengthPotion")
                    {
                        print("Drinking strength potion");
                        image.enabled = false;
                        image.sprite = null;
                        itemDragHandler.Item = null;
                        anim.SetTrigger("isDrinking");
                        StartCoroutine(waitForDrink("strength"));
                        break;
                    }
                    else if (itemObject.Name == "MithPotion")
                    {
                    	image.enabled = false;
                        image.sprite = null;
                        itemDragHandler.Item = null;
                    	anim.SetTrigger("isDrinking");
                        StartCoroutine(waitForDrink("strength"));
                        StartCoroutine(waitForDrink("speed"));
                        if (SceneManager.GetActiveScene().name == "win_scene") {
				            CanvasGroup winCanvas = GameObject.Find("Winning_Canvas").GetComponent<CanvasGroup>();
				            winCanvas.interactable = true;
				            winCanvas.alpha = 1f;
				        }
                    } else if (itemObject.Name == "HealthPotion") {
                    	image.enabled = false;
                        image.sprite = null;
                        itemDragHandler.Item = null;
                        anim.SetTrigger("isDrinking");
                        Slider playerHealth = (Slider) GameObject.Find("Slider").GetComponent<Slider>();
                        playerHealth.value += 0.3f;;
                        break;
                    }
                }
                else
                {
                    print("Empty itemobject for potion.  Nothing to drink.");
                }
            }
            
        }


        if (inputAction)
            Debug.Log("Action pressed");

    }

    public IEnumerator waitForDrink(string potion) {
        yield return new WaitForSeconds(2);
        if (potion == "hallucination") {
            if (SceneManager.GetActiveScene().name == "tutorial") {
                SceneManager.LoadScene("forest");
            } else if (SceneManager.GetActiveScene().name == "forest") {
                SceneManager.LoadScene("hallucination");
            } else if (SceneManager.GetActiveScene().name == "the_desert") {
                SceneManager.LoadScene("hallucination2");
            }

	    } else if (potion == "speed") {
	            isInSpeedBoost = true;
	    } else if (potion == "strength") {
            isInStrengthBoost = true;
            //this.transform.localScale += new Vector3(5f, 5f, 5f);
            StartCoroutine(growWizard(true));
	    }

    }

    public void growRoutine(bool grow)
    {
        StartCoroutine(growWizard(grow));
    }

    public IEnumerator growWizard(bool grow)
    {

        for (int i = 0; i <= 300; i++)
        {

            Vector3 scale = new Vector3(0.008f, 0.008f, 0.008f);


            GameObject go = new GameObject("ScalingParent");
            if (this.transform != null)
            {

                go.transform.parent = this.transform.parent;

                go.transform.Translate(this.transform.position);

                this.transform.parent = go.transform;

            }


            if (grow)
            {
                this.transform.localScale += scale;
            }
            else
            {
                go.transform.localScale -= scale;
                //this.transform.position = myPos;
            }
            yield return new WaitForSeconds(0);
        }
    }


     public IEnumerator waitForDig() {

        //int biome = GameObject.Find("Wizard_Holder").GetComponent<BiomeLocator>().getCurrentBiome();
        //print(biome);
     	int waitTime = 3;
     	if (isInSpeedBoost)
     		waitTime = 1;


        yield return new WaitForSeconds(waitTime);
        shovel.SetActive(false);
        GameObject sandLayer1 = GameObject.Find("Sand_Layers").transform.GetChild(0).gameObject;
        GameObject sandLayer2 = GameObject.Find("Sand_Layers").transform.GetChild(1).gameObject;
        GameObject sandLayer3 = GameObject.Find("Sand_Layers").transform.GetChild(2).gameObject;
        
        //gem2 pile
        bool pile2;

       

        //if near enough to pile 2
        if ((this.gameObject.transform.position - sandLayer3.transform.position).magnitude < 10f) {
        	pile2 = true;
        	if (pile2) {
            	if (sandLayer3.activeSelf == true) {
            		sandLayer3.SetActive(false);
            	} else if (sandLayer2.activeSelf == true) {
            		sandLayer2.SetActive(false);
            	} else if (sandLayer1.activeSelf == true) {
            		sandLayer1.SetActive(false);
            	}
            }
        }
    }

    public IEnumerator waitForThrow() {
     	float waitTime = 0.60f;
     	if (isInSpeedBoost)
     		waitTime = 0.3f;


        yield return new WaitForSeconds(waitTime);
        GameObject projectile = GameObject.Find("PlayerProjectile");

        Vector3 startPoint = GameObject.Find("mixamorig:Head").transform.position;

    	GameObject projectileOne = Instantiate(projectile,startPoint, Quaternion.identity);
		projectileOne.SetActive(true);
		Rigidbody pOne = projectileOne.GetComponent<Rigidbody>();
		
		Vector3 forceDirection = GameObject.Find("mixamorig:Head").transform.forward;
		pOne.AddForce(forceDirection.x * 25f, forceDirection.y - 5f, forceDirection.z * 25f, ForceMode.Impulse);
		float time = 3f;
		
        
    }
   
    //This is a physics callback
    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {

            ++groundContactCount;

            // Generate an event that might play a sound, generate a particle effect, etc.
            EventManager.TriggerEvent<PlayerLandsEvent, Vector3, float>(collision.contacts[0].point, collision.impulse.magnitude);

        }
						
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.transform.gameObject.tag == "ground")
        {
            --groundContactCount;
        }

    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition;
        Quaternion newRootRotation;

        bool isGrounded = IsGrounded || CharacterCommon.CheckGroundNear(this.transform.position, jumpableGroundNormalMaxAngle, 0.1f, 1f, out closeToJumpableGround);

        if (isGrounded)
        {
         	//use root motion as is if on the ground		
            newRootPosition = anim.rootPosition;
        }
        else
        {
            //Simple trick to keep model from climbing other rigidbodies that aren't the ground
            newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);
        }

        //use rotational root motion as is
        newRootRotation = anim.rootRotation;

        //Here, you could scale the difference in position and rotation to make the character go faster or slower
        this.transform.position = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMovementSpeed);
        this.transform.rotation = Quaternion.LerpUnclamped(this.transform.rotation, newRootRotation, rootTurnSpeed);
        //this.transform.position = newRootPosition;
        //this.transform.rotation = newRootRotation;

    }

    void OnAnimatorIK()
    {
        if(anim) {
            AnimatorStateInfo astate = anim.GetCurrentAnimatorStateInfo(0);
            if(astate.IsName("ButtonPress")) {
                float buttonWeight = anim.GetFloat("buttonClose");;
                // Set the look target position, if one has been assigned
                if(buttonObject != null) {
                    anim.SetLookAtWeight(buttonWeight);
                    anim.SetLookAtPosition(buttonObject.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand,buttonWeight);
                    anim.SetIKPosition(AvatarIKGoal.RightHand,buttonObject.transform.position);
                }
            }
            else
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                anim.SetLookAtWeight(0);
            }
        }
    }

}
