using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerloc = player.transform.position;
        //Debug.Log(playerloc);
        transform.position = new Vector3(playerloc.x, 50f, playerloc.z);
    }
}
