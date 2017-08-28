using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Specifies all player behaviours

    // Object references
    public PointerController pc;
    public CameraController cc;
    public Rigidbody rb;
    public LevelContent lvl;

    // Handy unity buttons for testing
    public bool isAtEndpoint = false;
    public bool isInDeathzone = false;
    public bool isSkippingHole = false;

    // Vars
    public float speed = 20;
    public float direction { get; set; }
    private double thresholdSpeed = 1;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        direction = 0;

        // Set trail
        if (SaveManager.Instance.playerTrails[SaveManager.Instance.state.activeTrail] != null)
        {
            GameObject trail = Instantiate(SaveManager.Instance.playerTrails[SaveManager.Instance.state.activeTrail] as GameObject);
            trail.transform.SetParent(rb.transform);
            trail.transform.localPosition = Vector3.zero;
        }

        // Set hat
        if (SaveManager.Instance.playerHats[SaveManager.Instance.state.activeHat] != null)
        {
            GameObject hat = Instantiate(SaveManager.Instance.playerHats[SaveManager.Instance.state.activeHat] as GameObject);
            hat.transform.SetParent(pc.transform);
            hat.transform.localPosition = new Vector3(0,0.5f,0); // Hat origin is 0,0,0 and player ball radius is 0.5
        }
    }

     void FixedUpdate()
    {
        var PlayerIsMoving = rb.velocity.magnitude > thresholdSpeed; // Used to limit when player can rotate
        var ValidBreathDetected = (UserInput.isExhaling() == true && UserInput.isValidBreath() == true);
        var UserIsPressingButton = UserInput.isHoldingButtonDown();

        if (PlayerIsMoving)
        {
            pc.showAsInactive(); pc.stopRotating();
            cc.UpdateDirection();
        }
        else
        {
            pc.showAsActive();
            if (UserIsPressingButton) { pc.Rotate(); direction = pc.getDirection(); }
            else { pc.stopRotating(); cc.UpdateDirection(); }
        }

        if (ValidBreathDetected) // Move player
        {
            var convertedDirection = direction * (float)Math.PI / 180;
            var forceDirection = new Vector3(speed * (float)Math.Sin(convertedDirection), 0.0f, speed * (float)Math.Cos(convertedDirection));
            rb.AddForce(forceDirection * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    { // Handles player collisions with specific object tags
        var collidedWithPickup = other.gameObject.CompareTag("Pickup");
        var reachedEndpoint = other.gameObject.CompareTag("Finish");
        var enteredDeathzone = other.gameObject.CompareTag("Deathzone");
        var pickedUpBoost = other.gameObject.CompareTag("Boost");

        if (collidedWithPickup)
        {
            other.gameObject.SetActive(false);
            lvl.CoinCollected();
        }
        if (reachedEndpoint) isAtEndpoint = true; else isAtEndpoint = false;
        if (enteredDeathzone) isInDeathzone = true; else isInDeathzone = false;
        if (pickedUpBoost) rb.AddForce(333333 * rb.velocity);

        // Note: Collider+Rigidbody = dynamic object
        // (else static, recalc/frame -> resource intense!)
    }

    public void CancelMomentum()
    {
        rb.velocity = Vector3.zero; rb.angularVelocity = Vector3.zero;
    }

}
