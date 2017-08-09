using UnityEngine;

public class EndpointRotator : MonoBehaviour {

    public float speed = 1;

    void Update ()
    {
        transform.Rotate(new Vector3(0, -30, 0) * speed * Time.deltaTime);
    }
}
