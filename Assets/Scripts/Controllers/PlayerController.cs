using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public PointerController pc;
    public Rigidbody rb;
    public LevelContent lvl;
    public float speed = 20;
    private float direction = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        // lvl = GetComponent<LevelContent>();
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
            lvl.EndpointReached();
        }
    }
}
