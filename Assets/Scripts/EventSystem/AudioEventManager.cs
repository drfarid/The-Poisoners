using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public AudioSource audio;
    private AudioSource effect;

    public AudioClip playerHitEffect;
    public AudioClip enemyHitEffect;
    public AudioClip digEffect;
    public AudioClip fireballEffect;

    private UnityAction<Vector3> enemyCloseEventListener;
    private UnityAction<Vector3> playerHitEventListener;
    private UnityAction<Vector3> enemyHitEventListener;
    private UnityAction<Vector3> fireballEventListener;


    void Awake()
    {

        enemyCloseEventListener = new UnityAction<Vector3>(enemyCloseEventHandler);
        playerHitEventListener = new UnityAction<Vector3>(playerHitEventHandler);
        enemyHitEventListener = new UnityAction<Vector3>(enemyHitEventHandler);
        fireballEventListener = new UnityAction<Vector3>(fireballEventHandler);

    }


    // Use this for initialization
    void Start()
    {
        effect = GetComponent<AudioSource>();
        			
    }


    void OnEnable()
    {

        EventManager.StartListening<EnemyCloseEvent, Vector3>(enemyCloseEventListener);
        EventManager.StartListening<PlayerHitEvent, Vector3>(playerHitEventListener);
        EventManager.StartListening<EnemyHitEvent, Vector3>(enemyHitEventListener);
        EventManager.StartListening<FireballEvent, Vector3>(fireballEventListener);

    }

    void OnDisable()
    {

        EventManager.StopListening<EnemyCloseEvent, Vector3>(enemyCloseEventListener);
        EventManager.StopListening<PlayerHitEvent, Vector3>(playerHitEventListener);
        EventManager.StopListening<EnemyHitEvent, Vector3>(enemyHitEventListener);
        EventManager.StopListening<FireballEvent, Vector3>(fireballEventListener);
    }


    void enemyCloseEventHandler(Vector3 worldPos)
    {
        //audio.PlayClipAtPoint(this.dangerMusic, worldPos);

    }

    void playerHitEventHandler(Vector3 worldPos)
    {
        effect.clip = this.playerHitEffect;
        effect.Play();

    }

    void enemyHitEventHandler(Vector3 worldPos)
    {
        effect.clip = this.enemyHitEffect;
        effect.Play();

    }

    void fireballEventHandler(Vector3 worldPos)
    {
        effect.clip = this.fireballEffect;
        effect.Play();

    }

}
