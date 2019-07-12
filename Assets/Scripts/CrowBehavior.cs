using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowBehavior : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    Vector3 positionOffset = new Vector3(2f, 3f, 0);


    // Start is called before the first frame update
    void Start()
    {
        this.Speak("this is a test message");
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
    }

    void Speak(string message)
    {
        Text messageText = canvas.GetComponent<Text>();
        messageText.text = message;
        Debug.Log(message);
    }

}
