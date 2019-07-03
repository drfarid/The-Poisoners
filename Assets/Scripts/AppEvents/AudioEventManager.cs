using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public AudioSource audio;

    public AudioClip defaultMusic;
    public AudioClip openingMusic;
    public AudioClip dangerMusic;
    public AudioClip hallucinationMusic;


    private UnityAction<Vector3> enemyCloseEventListener;


    void Awake()
    {

        enemyCloseEventListener = new UnityAction<Vector3>(enemyCloseEventHandler);

    }


    // Use this for initialization
    void Start()
    {


        			
    }


    void OnEnable()
    {

        EventManager.StartListening<EnemyCloseEvent, Vector3>(enemyCloseEventListener);

    }

    void OnDisable()
    {

        EventManager.StopListening<EnemyCloseEvent, Vector3>(enemyCloseEventListener);
    }


    void enemyCloseEventHandler(Vector3 worldPos)
    {
        AudioSource.PlayClipAtPoint(this.dangerMusic, worldPos);

    }

}
