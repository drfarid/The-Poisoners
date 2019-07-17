using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowBehavior : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    Vector3 heightOffset = new Vector3(0, 2.2f, 0);
    bool finishedMessage;
    public Image closedMouth;
    public Image openMouth;


    // Start is called before the first frame update
    void Start()
    {
        finishedMessage = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.Follow();
    }

    void Follow() 
    {
        // stay to right of player
        var leftOfPlayer = -Vector3.Cross(player.transform.forward, player.transform.up);
        this.transform.position = player.transform.position + leftOfPlayer + heightOffset;

        // face direction of player's movement
        Vector3 birdLookAt = player.transform.position + heightOffset + 5 * player.transform.forward;
        transform.LookAt(birdLookAt);
    }

    public void speak(string message)
    {
        Text messageText = canvas.GetComponent<Text>();
        messageText.text = message;
        finishedMessage = false;
        //Image crowAvatar = GameObject.Find("Crow_Avatar").GetComponent<Image>();
        Button x = canvas.GetComponentInChildren<Button>();
        x.gameObject.SetActive(true);

    }

    public void closeMessage()
    {
        finishedMessage = true;
        EventManager.TriggerEvent<CloseMessageEvent>();
        // hide X button
        Button x = canvas.GetComponentInChildren<Button>();
        x.gameObject.SetActive(false);
    }

    // public IEnumerator waitForMessage(int time) {
    //     yield return new WaitForSeconds(time);
    //     Text messageText = canvas.GetComponent<Text>();
    //     messageText.text = "";
    // }

    public void desertScript() {
        string message = "THIS IS A TEST event";
        Debug.Log(message);
    	EventManager.TriggerEvent<SpeakEvent, string>(message);
    }

}
