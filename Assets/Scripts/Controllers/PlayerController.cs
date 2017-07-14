using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // Object references
    public PointerController pc;
    public Rigidbody rb;

    public Text counttxt;
    public Text wintxt;
    public float speed = 20;

    private int pickupCount = 0;
    private float countdownValue = 3;
    private float direction = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        pickupCount = 0;
        UpdateCounter();
        wintxt.text = "";
	}

    void FixedUpdate() // called during physics events
    {

        if (rb.velocity.magnitude > 1)
        {
            pc.showAsInactive();
            pc.stopRotating();
        }
        else
        {
            pc.showAsActive();

            if (UserInput.isHoldingButtonDown()==true)
            {
                pc.Rotate();
                direction = pc.getDirection();
            }
            else
            {
                pc.stopRotating();
            }
        }

        if (UserInput.isExhaling()==true && UserInput.isValidBreath()==true)
        {
            var convertedDirection = direction * (float)Math.PI / 180;
            var forceDirection = new Vector3(speed * (float)Math.Sin(convertedDirection), 0.0f, speed * (float)Math.Cos(convertedDirection));
            rb.AddForce(forceDirection * speed);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // If player collides with another object, deactivate it
        // Collider+Regidbody = dynamic object (else static, recalc/frame -> resource intense!)
        var collidedWithPickup = other.gameObject.CompareTag("Pickup");

        if (collidedWithPickup)
        {
            other.gameObject.SetActive(false);
            pickupCount++;
            UpdateCounter();
        }

    }

    private void UpdateCounter()
    {
        counttxt.text = "Count: " + pickupCount.ToString();
        if (pickupCount >= 4)
        {
            wintxt.text = "WINNER!";
            StartCoroutine(StartCountdown(countdownValue));
        }
    }
    
    // Pause 3 seconds before going back to menu
    public IEnumerator StartCountdown(float countdownValue)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        SceneManager.LoadScene(0);
    }

}
