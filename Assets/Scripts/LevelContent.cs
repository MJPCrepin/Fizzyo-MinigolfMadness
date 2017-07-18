using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Generic Level object containing point and breath counters and collision detectors
// Each level shoud have its own child LevelContent object
public class LevelContent : MonoBehaviour {

    public PlayerController player;

    public Text currHole;
    public int currentHole = 0;

    public Text coinstxt;
    private int pickupCount = -1;

    public Text stroketxt;
    public int breathCount;

    public Text partxt;

    public Text wintxt;
    public int finalHole;

    private float timeBeforeNextLevel = 1f;
    private float timeBeforeMainMenu = 3f;

    private void Update()
    {
        breathCount=player.GetBreathCount();
        UpdateBreathCount();
    }

    public void initCounters()
    {
        UpdatePickupCounter();
        UpdateCurrHole();
        UpdateBreathCount();
    }

    public void PickupCollected()
    {
        UpdatePickupCounter();
    }

    public void UpdatePickupCounter()
    {
        pickupCount++;
        coinstxt.text = "Coins: " + pickupCount.ToString();
    }

    public int UpdateCurrHole()
    {
        currentHole++;
        currHole.text = "Hole " + currentHole.ToString();
        return currentHole;
    }

    public int UpdateBreathCount()
    {
        stroketxt.text = "Strokes: " + breathCount.ToString();
        return breathCount;
    }

    public void EndpointReached()
    {
        StartCoroutine(BackToMainMenu());
    }

    // Pause 3 seconds before going back to menu
    public IEnumerator BackToMainMenu()
    {
        float currCountdownValue = timeBeforeMainMenu;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        SceneManager.LoadScene(0);
    }

    public void SetNewPosition(float x, float y, float z)
    {
        player.CancelMomentum();
        player.transform.position = new UnityEngine.Vector3(x, y, z);
    }

}
