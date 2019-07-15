using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowBehavior : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    Vector3 positionOffset = new Vector3(2f, 3f, 0);
    bool finishedMessage;


    // Start is called before the first frame update
    void Start()
    {
        finishedMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.FlyTo();
    }

    void FlyTo() 
    {
        // face direction
        this.transform.position = player.transform.position + player.transform.forward + positionOffset;
        Vector3 birdLookAt = new Vector3(player.transform.position.x, player.transform.position.y + 3f, player.transform.position.z);
        transform.LookAt(birdLookAt);
    }

    public void Speak(string message, int duration)
    {
        Text messageText = canvas.GetComponent<Text>();
        messageText.text = message;
        StartCoroutine(waitForMessage(duration));
    }

    public IEnumerator waitForMessage(int time) {
        yield return new WaitForSeconds(time);
        Text messageText = canvas.GetComponent<Text>();
        messageText.text = "";
    }

    public void desertScript() {
    	Speak("1", 5);
    	StartCoroutine(waitForMessage(5));
    	Speak("2", 5);
    	StartCoroutine(waitForMessage(5));
    	Speak("3", 5);
    }

}
