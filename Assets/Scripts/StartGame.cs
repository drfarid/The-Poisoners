using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameStart() {
    	SceneManager.LoadScene("mixing_system");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
