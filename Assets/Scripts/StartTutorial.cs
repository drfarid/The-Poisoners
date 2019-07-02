using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void tutorialStart() {
    	SceneManager.LoadScene("game_alpha1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
