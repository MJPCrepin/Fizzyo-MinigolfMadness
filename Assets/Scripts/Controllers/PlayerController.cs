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
}
