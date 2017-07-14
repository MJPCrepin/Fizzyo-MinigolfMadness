using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {

    public GameObject player;
    private float speed = 0;
    private Vector3 offset;
    public PointerController pc;

    void Start()
    {
        offset = transform.position - player.transform.position;
        pc = GetComponent<PointerController>();
    }

    void LateUpdate ()
    {
        transform.Rotate(new Vector3(0, 30, 0) * speed * Time.deltaTime); // rotate
        transform.position = player.transform.position + offset; // player as centre
    }

    public void showAsActive()
    {
        pc.GetComponentInChildren<Renderer>().material.color = new Color(255, 0, 0, 0.1f);
    }

    public void showAsInactive()
    {
        pc.GetComponentInChildren<Renderer>().material.color = new Color(255, 255, 0, 0.1f);
    }

    public void Rotate()
    {
        speed = 10;
    }

    public void Rotate(int x)
    {
        speed = (float)x;
    }

    public void stopRotating()
    {
        speed = 0;
    }

    public bool isRotating()
    {
        return speed > 0;
    }

    public float getDirection()
    {
        return this.transform.eulerAngles.y;
    }
}
