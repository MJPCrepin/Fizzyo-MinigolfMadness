using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Generic Level object containing point and breath counters and collision detectors
// Each level shoud have its own child LevelContent object
public class LevelContent : MonoBehaviour {

    public Rigidbody rb;
    public Text counttxt;
    public Text wintxt;
    private int pickupCount = 0;
    private float countdownValue = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        UpdateCounter();
        wintxt.text = "";
    }

    private void UpdateCounter()
    {
        counttxt.text = "Count: " + pickupCount.ToString();
        if (pickupCount >= 4)
        {
            wintxt.text = "WINNER!";
            //StartCoroutine(StartCountdown(countdownValue));
        }
    }

}
