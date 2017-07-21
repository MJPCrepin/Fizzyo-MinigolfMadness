using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePage : MonoBehaviour {

    public RectTransform menuContainer;
    public Transform levelPanel;

    public Transform colourScrollview;
    public Transform hatScrollview;
    public Transform trailScrollview;


    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 2f;

    private Vector3 desiredMenuPosition;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
        InitShop();
        InitLevel();
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }

    private void InitShop()
    {
        if (colourScrollview == null || trailScrollview == null || hatScrollview == null)
            Debug.Log("Shop scroll ref missing");

        int i = 0;
        foreach (Transform shopItem in colourScrollview)
        {
            int currentIndex = i;
            Button b = shopItem.GetComponent<Button>();
            Debug.Log("Colour selected: " + currentIndex);
            b.onClick.AddListener(() => OnColourSelect(currentIndex));
            i++;
        }

        i = 0;
        foreach (Transform shopItem in trailScrollview)
        {
            int currentIndex = i;
            Button b = shopItem.GetComponent<Button>();
            Debug.Log("Trail selected: " + currentIndex);
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));
            i++;
        }

        i = 0;
        foreach (Transform shopItem in hatScrollview)
        {
            int currentIndex = i;
            Button b = shopItem.GetComponent<Button>();
            Debug.Log("Hat selected: " + currentIndex);
            b.onClick.AddListener(() => OnHatSelect(currentIndex));
            i++;
        }
    }

    private void InitLevel()
    {
        if (levelPanel == null)
            Debug.Log("lvlPanel ref missing");

        int i = 0;
        foreach (Transform level in levelPanel)
        {
            int currentIndex = i;
            Button b = level.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            i++;
        }
    }

    private void OnLevelSelect(int currentIndex)
    {
        switch (currentIndex)
        {
            case 0: SceneManager.LoadScene(1); break;
            default: Debug.Log("Level selected: " + currentIndex); break;
        }
        
    }

    private void OnColourSelect(int currentIndex)
    {
        Debug.Log("Selecting colour: " + currentIndex);
    }

    private void OnTrailSelect(int currentIndex)
    {
        Debug.Log("Selecting trail: " + currentIndex);
    }

    private void OnHatSelect(int currentIndex)
    {
        Debug.Log("Selecting hat: " + currentIndex);
    }

    public void OnColourBuyOrSet()
    {
        Debug.Log("Buy/set colour");
    }

    public void OnTrailBuyOrSet()
    {
        Debug.Log("Buy/set trail");
    }

    public void OnHatBuyOrSet()
    {
        Debug.Log("Buy/set hat");
    }

    //Menu transitions
    public void GoBackToMainMenu()
    {
        NavigateTo(0);
    }

    public void GoToLevelSelect()
    {
        NavigateTo(1);
    }

    public void GoToShop()
    {
        NavigateTo(2);
    }

    public void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {
            default:
            case 0: desiredMenuPosition = Vector3.zero; break;
            case 1: desiredMenuPosition = Vector3.right * 1280; break;
            case 2: desiredMenuPosition = Vector3.left * 1280; break;
        }
    }
	
	// Update is called once per frame
	public void QuitGame ()
    {
        Application.Quit();
	}
}