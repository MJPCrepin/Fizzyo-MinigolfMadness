using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePage : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 2f;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
	}

    public void GoToShop()
    {
        Debug.Log("Shop button clicked");
    }
	
	// Update is called once per frame
	public void QuitGame ()
    {
        Application.Quit();
	}
}