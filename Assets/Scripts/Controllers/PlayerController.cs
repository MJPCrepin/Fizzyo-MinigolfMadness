using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PointerController pc;
    public CameraController cc;
    public Rigidbody rb;
    public LevelContent lvl;
    public bool isAtEndpoint = false;
    public bool isInDeathzone = false;
    public bool isSkippingHole = false;
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
        //TODO
    }

     void FixedUpdate()
    {
        var PlayerIsMoving = rb.velocity.magnitude > thresholdSpeed;
        var ValidBreathDetected = (UserInput.isExhaling() == true && UserInput.isValidBreath() == true);

        if (PlayerIsMoving)
        {
            pc.showAsInactive(); pc.stopRotating();
            cc.UpdateDirection();
        }
        else
        {
            pc.showAsActive();
            if (UserInput.isHoldingButtonDown() == true) { pc.Rotate(); direction = pc.getDirection(); }
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
    {   // Collider+Regidbody = dynamic object (else static, recalc/frame -> resource intense!)
        var collidedWithPickup = other.gameObject.CompareTag("Pickup");
        var reachedEndpoint = other.gameObject.CompareTag("Finish");
        var enteredDeathzone = other.gameObject.CompareTag("Deathzone");

        if (collidedWithPickup)
        {
            other.gameObject.SetActive(false);
            lvl.CoinCollected();
        }
        if (reachedEndpoint) isAtEndpoint = true; else isAtEndpoint = false;
        if (enteredDeathzone) isInDeathzone = true; else isInDeathzone = false;
    }

    public void CancelMomentum()
    {
        rb.velocity = Vector3.zero; rb.angularVelocity = Vector3.zero;
    }

}
