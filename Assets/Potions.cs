using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Image blockHealth = GameObject.Find("Block_Health").GetComponent<Image>();
        blockHealth.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
