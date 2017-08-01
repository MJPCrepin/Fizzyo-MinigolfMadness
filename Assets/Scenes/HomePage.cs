using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePage : MonoBehaviour {

    public RectTransform menuContainer;
    public Transform levelPanel;
    public GameObject playerPreview;

    private GameObject currentTrail;
    private GameObject currentHat;

    public Transform colourScrollview;
    public Transform hatScrollview;
    public Transform trailScrollview;

    public Text colourBuySetTxt;
    public Text hatBuySetTxt;
    public Text trailBuySetTxt;
    public Text coinsTxt;

    private int[] colourPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private int[] hatPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private int[] trailPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

    private int selectedColourIndex;
    private int selectedHatIndex;
    private int selectedTrailIndex;
    private int activeColourIndex;
    private int activeHatIndex;
    private int activeTrailIndex;

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 2f;
    private Vector3 desiredMenuPosition;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
        InitShop();
        InitLevel();
        InitPref();
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }

    // Initialisers

    private void InitPref()
    {
        var activeColour = SaveManager.Instance.state.activeColour;
        var activeTrail = SaveManager.Instance.state.activeTrail;
        var activeHat = SaveManager.Instance.state.activeHat;

        OnColourSelect(activeColour);
        SetColour(activeColour);
        colourScrollview.GetChild(activeColour).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;

        OnTrailSelect(activeTrail);
        SetTrail(activeTrail);
        trailScrollview.GetChild(activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;

        OnHatSelect(activeHat);
        SetHat(activeHat);
        hatScrollview.GetChild(activeHat).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
    }

    private void InitShop()
    {
        UpdateCoinsText();

        if (colourScrollview == null || trailScrollview == null || hatScrollview == null)
            Debug.Log("Shop scroll ref missing");

        int i = 0;
        foreach (Transform shopItem in colourScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsColourOwned(i);
            
            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnColourSelect(currentIndex));

            shopItem.GetComponentInChildren<Text>().text = "Locked";

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned
                ? SaveManager.Instance.playerColours[currentIndex]
                : Color.Lerp(SaveManager.Instance.playerColours[currentIndex], Color.grey, 0.25f);

            i++;
        }

        i = 0;
        foreach (Transform shopItem in trailScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsTrailOwned(i);

            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned
                ? SaveManager.Instance.playerColours[currentIndex]
                : Color.Lerp(SaveManager.Instance.playerColours[currentIndex], Color.grey, 0.25f);

            i++;
        }

        i = 0;
        foreach (Transform shopItem in hatScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsHatOwned(i);

            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnHatSelect(currentIndex));

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned ? Color.white : new Color(0.7f, 0.7f, 0.7f);

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

    private void UpdateCoinsText()
    {
        coinsTxt.text = SaveManager.Instance.state.coins.ToString();
    }

    // Shop buttons

    private void OnLevelSelect(int currentIndex)
    {
        switch (currentIndex)
        {
            case 0: SceneManager.LoadScene(1); break;
            case 1: SceneManager.LoadScene(2); break;
            default: Debug.Log("Level selected: " + currentIndex); break;
        }
        
    }

    private void OnColourSelect(int currentIndex)
    {
        if (selectedColourIndex == currentIndex)
            return;
        colourScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        colourScrollview.GetChild(selectedColourIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedColourIndex = currentIndex;
        var colourIsOwned = SaveManager.Instance.IsColourOwned(currentIndex);

        if (colourIsOwned)
        {
            if (activeColourIndex == currentIndex) colourBuySetTxt.text = "Equipped";
            else colourBuySetTxt.text = "Equip";
        }
        else
        {
            colourBuySetTxt.text = "Buy: " + colourPrice[currentIndex].ToString();
        }
    }

    private void OnTrailSelect(int currentIndex)
    {
        if (selectedTrailIndex == currentIndex)
            return;
        trailScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        trailScrollview.GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedTrailIndex = currentIndex;
        var trailIsOwned = SaveManager.Instance.IsTrailOwned(currentIndex);

        if (trailIsOwned)
        {
            if (activeTrailIndex == currentIndex) trailBuySetTxt.text = "Equipped";
            else trailBuySetTxt.text = "Equip";
        }
        else
        {
            trailBuySetTxt.text = "Buy: " + trailPrice[currentIndex].ToString();
        }
    }

    private void OnHatSelect(int currentIndex)
    {
        if (selectedHatIndex == currentIndex)
            return;
        hatScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        hatScrollview.GetChild(selectedHatIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedHatIndex = currentIndex;
        var hatIsOwned = SaveManager.Instance.IsHatOwned(currentIndex);

        if (hatIsOwned)
        {
            if (activeHatIndex == currentIndex) hatBuySetTxt.text = "Equipped";
            else hatBuySetTxt.text = "Equip";
        }
        else
        {
            hatBuySetTxt.text = "Buy: " + hatPrice[currentIndex].ToString();
        }
    }

    // Shop event handlers

    public void OnColourBuyOrSet()
    {
        var colourIsOwned = SaveManager.Instance.IsColourOwned(selectedColourIndex);
        var canPurchase = SaveManager.Instance.BuyColour(selectedColourIndex, colourPrice[selectedColourIndex]);

        if (colourIsOwned) SetColour(selectedColourIndex);
        else
        {
            if (canPurchase)
            {
                SetColour(selectedColourIndex);
                colourScrollview.GetChild(selectedColourIndex).GetComponent<Image>().color = SaveManager.Instance.playerColours[selectedColourIndex];
                colourScrollview.GetChild(selectedColourIndex).GetComponentInChildren<Text>().text = "";
                UpdateCoinsText();
            }
            else
            {
                // Feedback sound?
                Debug.Log("Not enough coins");
            }
        }
    }

    public void OnHatBuyOrSet()
    {
        var hatIsOwned = SaveManager.Instance.IsHatOwned(selectedHatIndex);
        var canPurchase = SaveManager.Instance.BuyHat(selectedHatIndex, hatPrice[selectedHatIndex]);

        if (hatIsOwned) SetHat(selectedHatIndex);
        else
        {
            if (canPurchase)
            {
                SetHat(selectedHatIndex);
                hatScrollview.GetChild(selectedHatIndex).GetComponent<Image>().color = Color.white;
                UpdateCoinsText();
            }
            else
            {
                // Feedback sound?
                Debug.Log("Not enough coins");
            }
        }
    }

    public void OnTrailBuyOrSet()
    {
        var trailIsOwned = SaveManager.Instance.IsTrailOwned(selectedTrailIndex);
        var canPurchase = SaveManager.Instance.BuyTrail(selectedTrailIndex, hatPrice[selectedTrailIndex]);

        if (trailIsOwned) SetTrail(selectedTrailIndex);
        else
        {
            if (canPurchase)
            {
                SetTrail(selectedTrailIndex);
                trailScrollview.GetChild(selectedTrailIndex).GetComponent<Image>().color = Color.white;
                UpdateCoinsText();
            }
            else
            {
                // Feedback sound?
                Debug.Log("Not enough coins");
            }
        }
    }

    // Shop setters

    public void SetColour(int index)
    {
        activeColourIndex = index;
        SaveManager.Instance.state.activeColour = index;
        SaveManager.Instance.playerMaterial.color = SaveManager.Instance.playerColours[index];
        SaveManager.Instance.Save();
        colourBuySetTxt.text = "Equipped";
    }

    public void SetHat(int index)
    {
        activeHatIndex = index;
        SaveManager.Instance.state.activeHat = index;
        hatBuySetTxt.text = "Equipped";
        SaveManager.Instance.Save();
    }

    public void SetTrail(int index)
    {
        activeTrailIndex = index;
        SaveManager.Instance.state.activeTrail = index;

        if (currentTrail != null) Destroy(currentTrail);
        currentTrail = Instantiate(SaveManager.Instance.playerTrails[index] as GameObject);
        currentTrail.transform.SetParent(playerPreview.transform);
        currentTrail.transform.localPosition = Vector3.zero;

        trailBuySetTxt.text = "Equipped";
        SaveManager.Instance.Save();
    }

    // Menu transitions

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
            case 0: desiredMenuPosition = Vector3.zero; break; // Main
            case 1: desiredMenuPosition = Vector3.right * 1280; break; // Levels
            case 2: desiredMenuPosition = Vector3.left * 1280; break; // Shop
        }
    }
	
	public void QuitGame ()
    {
        Application.Quit();
	}
}