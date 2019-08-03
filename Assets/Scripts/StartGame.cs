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
    	SceneManager.LoadScene("forest");
    }

    public void MultiStart()
    {
        SceneManager.LoadScene("multiplayer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
