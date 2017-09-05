using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Generic Level object with common values and functions
// Each level shoud have its own child LevelContent object
public class LevelContent : MonoBehaviour {

    public PlayerController player;
    public Transform homeButton;

    public Text currHole;
    public int currentHole = 0;

    public Text totalCoinstxt;
    public Text coinstxt;
    private int pickupCount = 0;

    public Text stroketxt;
    private int strokeCount = -1;

    public Text partxt;
    private int parNumber { get; set; }

    public Text wintxt;
    public GameObject popup;

    // TODO: Check if this works correctly with Fizzyo device (increment per breath, not update)
    public void DetectBreathTrigger()
    {
        if (UserInput.aBreathIsDetected())
            UpdateStrokeCount();
    }

    public void initCounters()
    { // Populates textboxes on level load
        UpdateCoinCounter(0);
        UpdateCurrHole();
        UpdateStrokeCount();
        UpdateTotalCoins();
    }

    // COIN FUNCTIONS

    public void CoinCollected()
    { // Used during coin collision event
        UpdateCoinCounter(1);
    }

    public void UpdateCoinCounter(int x)
    { // Where x is the value by which the counter changes (can be negative)
        pickupCount+=x;
        coinstxt.text = "Coins: " + pickupCount.ToString();
        UpdateTotalCoins();
    }

    public void UpdateTotalCoins()
    { // Displays player coin balance (incl coins collected this level)
        totalCoinstxt.text = "Total: " + (SaveManager.Instance.state.coins+pickupCount).ToString();
    }

    // HOLE FUNCTIONS

    public int UpdateCurrHole()
    { // Used when player reaches an end point
        currentHole++;
        currHole.text = "Hole " + currentHole.ToString();
        return currentHole;
    }

    public int SetPar(int x)
    { // Child should call this for every new hole
        parNumber = x;
        partxt.text = "Par: " + parNumber.ToString();
        return parNumber;
    }

    public void EndpointReached()
    {
        player.isAtEndpoint = false;
        UpdateCurrHole();
        DisplayGolfScore();
        strokeCount = -1;
        UpdateStrokeCount();
    }

    public void SkipHole()
    {
        if (SaveManager.Instance.state.coins >= currentHole)
        { // Check if player can afford it (price = hole number)
            player.isSkippingHole = true;
            StartCoroutine(ShowPopup("Skipped hole (-"+currentHole+" coins)", 1.5f));
            UpdateCoinCounter(-currentHole);
            UpdateCurrHole();
            strokeCount = -1;
            UpdateStrokeCount();
        }
        else
        {
            player.isSkippingHole = false;
            StartCoroutine(ShowPopup("Not enough coins to skip!", 1.5f));
        }
        
    }

    public void DisplayGolfScore()
    { // Shows appropriate golf term at the end of a hole
        if (strokeCount == 1)
        {
            StartCoroutine(ShowPopup("ACE!", 1.5f));
        }
        else
        {
            switch (strokeCount - parNumber)
            {
                case 3: StartCoroutine(ShowPopup("Triple Bogey", 1.5f)); break;
                case 2: StartCoroutine(ShowPopup("Double Bogey", 1.5f)); break;
                case 1: StartCoroutine(ShowPopup("Bogey", 1.5f)); break;
                case 0: StartCoroutine(ShowPopup("Par", 1.5f)); break;
                case -1: StartCoroutine(ShowPopup("Birdie", 1.5f)); break;
                case -2: StartCoroutine(ShowPopup("Eagle", 1.5f)); break;
                case -3: StartCoroutine(ShowPopup("Albatross", 1.5f)); break;
                case -4: StartCoroutine(ShowPopup("Condor", 1.5f)); break;
                default: break;
            }
        }
    }

    public void SetNewPosition(float x, float y, float z)
    { // Used for teleports (eg new hole)
        player.CancelMomentum(); // Avoid Portal effect
        player.transform.position = new Vector3(x, y, z);
    }

    // POPUP FUNCTIONS

    public void ShowPopup(string msg)
    {
        popup.SetActive(true);
        wintxt.text = msg;
    }

    public IEnumerator ShowPopup(string msg, float time)
    { // Example: StartCoroutine(ShowPopup("Win", 3f));
        ShowPopup(msg);
        float currCountdownValue = time;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        HidePopup();
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

    // OTHER FUNCTIONS

    public int UpdateStrokeCount()
    {
        strokeCount++;
        stroketxt.text = "Strokes: " + strokeCount.ToString();
        return strokeCount;
    }

    public IEnumerator BackToMainMenu()
    { // Pause 3 seconds before going back to menu, save state
        float currCountdownValue = 3f;
        while (currCountdownValue > 0)
        {
            ShowPopup("Returning to Main Menu");
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        HidePopup();
        SaveManager.Instance.state.coins += pickupCount;
        SaveManager.Instance.Save();
        SceneManager.LoadScene(0);
    }

    public void BackToHomePage()
    {
        SceneManager.LoadScene(0);
    }

}
