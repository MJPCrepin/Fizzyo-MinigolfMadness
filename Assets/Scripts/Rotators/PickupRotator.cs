using UnityEngine;

public class PickupRotator : MonoBehaviour {

    public float speed = 1;

	void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * speed * Time.deltaTime);
	}

}
