using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrowEventManager : MonoBehaviour
{

    public UnityAction<string> speakEventListener;
    public UnityAction closeMessageEventListener;


    void Awake()
    {

        speakEventListener = new UnityAction<string>(speakEventHandler);
        closeMessageEventListener = new UnityAction(closeMessageEventHandler);

    }

    void Start()
    {
        			
    }


    void OnEnable()
    {

        EventManager.StartListening<SpeakEvent, string>(speakEventListener);
        EventManager.StartListening<CloseMessageEvent>(closeMessageEventListener);

    }

    void OnDisable()
    {

        EventManager.StopListening<SpeakEvent, string>(speakEventListener);
        EventManager.StopListening<CloseMessageEvent>(closeMessageEventListener);
    }


    // trigger by using EventManager.TriggerEvent<SpeakEvent, string>(message);
    void speakEventHandler(string message)
    {
        Debug.Log(message);
        CrowBehavior crow = GameObject.Find("Crow_GameObject").GetComponent<CrowBehavior>();
        crow.speak(message);

    }

    void closeMessageEventHandler()
    {
        Debug.Log("CLOSE MESSAGE CLICKED");
        CrowBehavior crow = GameObject.Find("Crow_GameObject").GetComponent<CrowBehavior>();
        crow.speak("");
    }

}
