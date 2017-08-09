using UnityEngine;

public class PointerController : MonoBehaviour {

    // Controls behaviour of the direction pointer attached to player

    public GameObject player;
    private float speed = 0;
    private Vector3 offset; // playerpointer distance
    public PointerController pc;

    // TODO: add lerp to smoothen rotation

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
    { // Turn red when active
        pc.GetComponentInChildren<Renderer>().material.color = new Color(255, 0, 0, 0.1f);
    }

    public void showAsInactive()
    { // Turn yellow when inactive
        pc.GetComponentInChildren<Renderer>().material.color = new Color(255, 255, 0, 0.1f);
    }

    public void Rotate()
    { // Default
        speed = 10;
    }

    public void Rotate(float x)
    {
        speed = x;
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
        return transform.eulerAngles.y;
    }
}
