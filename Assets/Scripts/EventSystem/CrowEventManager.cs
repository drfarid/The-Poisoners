using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrowEventManager : MonoBehaviour
{

    public UnityAction<string> speakEventListener;
    public UnityAction closeMessageEventListener;

    private AudioSource crowAudio;
    public AudioClip crowSquak;


    void Awake()
    {

        speakEventListener = new UnityAction<string>(speakEventHandler);
        closeMessageEventListener = new UnityAction(closeMessageEventHandler);

    }

    void Start()
    {
        crowAudio = GetComponent<AudioSource>();
        crowAudio.clip = this.crowSquak;	
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
        crowAudio.Play();

    }

    void closeMessageEventHandler()
    {
        CrowBehavior crow = GameObject.Find("Crow_GameObject").GetComponent<CrowBehavior>();
        crow.speak("");
    }

}
