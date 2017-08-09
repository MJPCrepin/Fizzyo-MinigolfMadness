using UnityEngine;

public class ObstacleRotator : MonoBehaviour {

    public float speed = 1;

    void Update ()
    {
        transform.Rotate(new Vector3(0, 30, 0) * speed * Time.deltaTime);
    }
}
