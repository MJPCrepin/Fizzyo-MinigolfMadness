using UnityEngine;

public class PaddleRotator : MonoBehaviour {

    public float speed = 1;

    void Update ()
    {
        transform.Rotate(new Vector3(30, 0, 0) * speed * Time.deltaTime);
    }
}
