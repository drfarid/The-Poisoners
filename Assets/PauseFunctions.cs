using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void RestartLevel() {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
    	Application.Quit();
    }
}
