using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public PointerController pc;
    public CameraController cc;
    public Rigidbody rb;
    public LevelContent lvl;
    public bool isAtEndpoint = false;
    public float speed = 20;
    private float direction = 0;
    private double thresholdSpeed = 1;
    private int breathCount = 0;

    //Values used for breath counter emulation
    private bool breathStarted = false;
    private bool breathEnded = false;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate() // called during physics events
    {
        if (rb.velocity.magnitude > thresholdSpeed)
        {
            pc.showAsInactive();
            pc.stopRotating();
            cc.UpdateDirection();
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
                cc.UpdateDirection();
            }
        }

        if (UserInput.isExhaling()==true && UserInput.isValidBreath()==true)
        {
            var convertedDirection = direction * (float)Math.PI / 180;
            var forceDirection = new Vector3(speed * (float)Math.Sin(convertedDirection), 0.0f, speed * (float)Math.Cos(convertedDirection));
            rb.AddForce(forceDirection * speed);
        }

        // Doesn't work
        IncrementBreathCounter();
    }

    private void OnTriggerEnter(Collider other)
    {   // Collider+Regidbody = dynamic object (else static, recalc/frame -> resource intense!)
        var collidedWithPickup = other.gameObject.CompareTag("Pickup");
        var reachedEndpoint = other.gameObject.CompareTag("Finish");

        // If player collides with another object, deactivate it
        if (collidedWithPickup)
        {
            other.gameObject.SetActive(false);
            lvl.PickupCollected();
        }

        if(reachedEndpoint)
        {
            isAtEndpoint = true;
        }
        else
        {
            isAtEndpoint = false;
        }
    }

    public void CancelMomentum()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // Second method to be subscribed to a breath counter
    public void IncrementBreathCounter() //Simulator not working
    {
        if (UserInput.isExhaling()==true)
        {
            breathStarted = true;
        }
        if (UserInput.isExhaling()!=true)
        {
            breathEnded = true;
        }
        if (breathStarted == true && breathEnded == true)
        {
            breathCount++;
            breathStarted = false;
            breathEnded = false;
        }
    }
    private void IncrementBreathCounter(object sender, EventArgs args)
    {
        UserInput.getBreathCount();
    }

    public void ResetBreathCounter()
    {
        breathCount = 0;
    }

    public int GetBreathCount()
    {
        return breathCount;
    }

}
