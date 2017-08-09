using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePage : MonoBehaviour {

    // Visual menu objects
    public RectTransform menuContainer;
    public Transform levelPanel;
    public GameObject playerPreview;
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 2f;
    private Vector3 desiredMenuPosition;

    // Equipped player items (colour is a material)
    private GameObject currentTrail;
    private GameObject currentHat;

    // Shop item scrollers
    public Transform colourScrollview;
    public Transform hatScrollview;
    public Transform trailScrollview;

    // Shop dynamic text gameobjects
    public Text colourBuySetTxt;
    public Text hatBuySetTxt;
    public Text trailBuySetTxt;
    public Text coinsTxt;

    // Prices for items in shop
    private int[] colourPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private int[] hatPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private int[] trailPrice = new int[] { 0, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

    // Shop button/item indexes
    private int selectedColourIndex;
    private int selectedHatIndex;
    private int selectedTrailIndex;
    private int activeColourIndex;
    private int activeHatIndex;
    private int activeTrailIndex;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1;
        InitShop();
        InitLevel();
        InitPref();
    }

    private void Update()
    { // Used to fade into menu and move between menu pages
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }

    // INITIALISERS

    private void InitPref()
    { // Load last equipped items
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

        // Check for missing unity references
        if (colourScrollview == null || trailScrollview == null || hatScrollview == null)
            Debug.Log("Shop scroll ref missing");

        int i = 0; // Initialise colour buttons
        foreach (Transform shopItem in colourScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsColourOwned(i);
            
            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnColourSelect(currentIndex));

            if (currentIndex > 0) shopItem.GetComponentInChildren<Text>().text = "Locked";

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned
                ? SaveManager.Instance.playerColours[currentIndex]
                : Color.Lerp(SaveManager.Instance.playerColours[currentIndex], Color.grey, 0.25f);

            i++;
        }

        i = 0; // Initialise trail buttons
        foreach (Transform shopItem in trailScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsTrailOwned(i);

            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));

            if (currentIndex > 0) shopItem.GetComponentInChildren<Text>().text = "Locked";

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned
                ? SaveManager.Instance.playerColours[currentIndex]
                : Color.Lerp(SaveManager.Instance.playerColours[currentIndex], Color.grey, 0.25f);

            i++;
        }

        i = 0; // Initialise hat buttons
        foreach (Transform shopItem in hatScrollview)
        {
            int currentIndex = i;
            var isOwned = SaveManager.Instance.IsHatOwned(i);

            Button b = shopItem.GetComponent<Button>();
            b.onClick.AddListener(() => OnHatSelect(currentIndex));

            if (currentIndex > 0) shopItem.GetComponentInChildren<Text>().text = "Locked";

            Image img = shopItem.GetComponent<Image>();
            img.color = isOwned ? Color.white : new Color(0.7f, 0.7f, 0.7f);

            i++;
        }
    }

    private void InitLevel()
    {
        if (levelPanel == null) // Check for missing unity references
            Debug.Log("lvlPanel ref missing");

        int i = 0; // Add listeners to level buttons
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

    // SHOP BUTTONS

    private void OnColourSelect(int currentIndex)
    {
        if (selectedColourIndex == currentIndex)
            return; // Don't change colour

        // Enlarge selected colour button
        colourScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        colourScrollview.GetChild(selectedColourIndex).GetComponent<RectTransform>().localScale = Vector3.one;
        selectedColourIndex = currentIndex;

        var colourIsOwned = SaveManager.Instance.IsColourOwned(currentIndex);

        if (colourIsOwned)
        { // Show as equipped or allow to
            if (activeColourIndex == currentIndex) colourBuySetTxt.text = "Equipped";
            else colourBuySetTxt.text = "Equip";
        }
        else
        { // Show price
            colourBuySetTxt.text = "Buy: " + colourPrice[currentIndex].ToString();
        }
    }

    private void OnTrailSelect(int currentIndex)
    {
        if (selectedTrailIndex == currentIndex)
            return; // Don't change trail

        // Enlarge selected trail button
        trailScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        trailScrollview.GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one;
        selectedTrailIndex = currentIndex;

        var trailIsOwned = SaveManager.Instance.IsTrailOwned(currentIndex);

        if (trailIsOwned)
        { // Show as equipped or allow to
            if (activeTrailIndex == currentIndex) trailBuySetTxt.text = "Equipped";
            else trailBuySetTxt.text = "Equip";
        }
        else
        { // Show price
            trailBuySetTxt.text = "Buy: " + trailPrice[currentIndex].ToString();
        }
    }

    private void OnHatSelect(int currentIndex)
    {
        if (selectedHatIndex == currentIndex)
            return; // Don't change hat

        // Enlarge selected hat button
        hatScrollview.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
        hatScrollview.GetChild(selectedHatIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedHatIndex = currentIndex;
        var hatIsOwned = SaveManager.Instance.IsHatOwned(currentIndex);

        if (hatIsOwned)
        { // Show as equipped or allow to
            if (activeHatIndex == currentIndex) hatBuySetTxt.text = "Equipped";
            else hatBuySetTxt.text = "Equip";
        }
        else
        { // Show price
            hatBuySetTxt.text = "Buy: " + hatPrice[currentIndex].ToString();
        }
    }

    // SHOP EVENT HANDLERS

    public void OnColourBuyOrSet()
    {
        var colourIsOwned = SaveManager.Instance.IsColourOwned(selectedColourIndex);
        var canPurchase = SaveManager.Instance.BuyColour(selectedColourIndex, colourPrice[selectedColourIndex]);

        if (colourIsOwned) SetColour(selectedColourIndex);
        else
        {
            if (canPurchase)
            { // Unlock item (remove shadow and locked text)
                SetColour(selectedColourIndex);
                colourScrollview.GetChild(selectedColourIndex).GetComponent<Image>().color = SaveManager.Instance.playerColours[selectedColourIndex];
                colourScrollview.GetChild(selectedColourIndex).GetComponentInChildren<Text>().text = "";
                UpdateCoinsText();
            }
            else
            { 
                // Feedback/sound?
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
            { // Unlock item (remove shadow and locked text)
                SetHat(selectedHatIndex);
                hatScrollview.GetChild(selectedHatIndex).GetComponentInChildren<Text>().text = "";
                UpdateCoinsText();
            }
            else
            {
                // Feedback/sound?
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
            { // Unlock item (remove shadow and locked text)
                SetTrail(selectedTrailIndex);
                trailScrollview.GetChild(selectedTrailIndex).GetComponent<Image>().color = SaveManager.Instance.playerColours[selectedTrailIndex];
                trailScrollview.GetChild(selectedTrailIndex).GetComponentInChildren<Text>().text = "";
                UpdateCoinsText();
            }
            else
            {
                // Feedback/sound?
                Debug.Log("Not enough coins");
            }
        }
    }

    // SHOP SETTERS

    public void SetColour(int index)
    { // Ball will always have a colour (default white, index 0)
        activeColourIndex = index;
        SaveManager.Instance.state.activeColour = index;
        SaveManager.Instance.playerMaterial.color = SaveManager.Instance.playerColours[index];

        colourBuySetTxt.text = "Equipped";
        SaveManager.Instance.Save();
    }

    public void SetHat(int index)
    { // Default hat is null
        activeHatIndex = index;
        SaveManager.Instance.state.activeHat = index;

        if(currentHat != null) Destroy(currentHat);
        currentHat = Instantiate(SaveManager.Instance.playerHats[index] as GameObject);
        currentHat.transform.SetParent(playerPreview.transform);
        currentHat.transform.localScale = new Vector3(1f,1f,1f);
        currentHat.transform.localPosition = Vector3.zero;

        hatBuySetTxt.text = "Equipped";
        SaveManager.Instance.Save();
    }

    public void SetTrail(int index)
    { // Default trail is null
        activeTrailIndex = index;
        SaveManager.Instance.state.activeTrail = index;

        if (currentTrail != null) Destroy(currentTrail);
        currentTrail = Instantiate(SaveManager.Instance.playerTrails[index] as GameObject);
        currentTrail.transform.SetParent(playerPreview.transform);
        currentTrail.transform.localPosition = Vector3.zero;

        trailBuySetTxt.text = "Equipped";
        SaveManager.Instance.Save();
    }

    // MENU NAVIGATION

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

    private void OnLevelSelect(int currentIndex)
    { // Links level buttons to scenes
        switch (currentIndex)
        {
            case 0: SceneManager.LoadScene(1); break;
            case 1: SceneManager.LoadScene(2); break;
            default: Debug.Log("Level selected (" + currentIndex + ") does not ref a scene!"); break;
        }
    }

    public void NavigateTo(int menuIndex)
    { // Move camera to menu pages
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