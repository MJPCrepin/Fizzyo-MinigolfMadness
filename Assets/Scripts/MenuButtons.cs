using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public void PlayGame ()
    {
        SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	public void QuitGame ()
    {
        Application.Quit();
	}
}