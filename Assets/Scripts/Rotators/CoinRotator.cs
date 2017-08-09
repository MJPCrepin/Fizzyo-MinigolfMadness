using UnityEngine;

public class CoinRotator : MonoBehaviour {

    public float speed = 1;

	void Update ()
    {
        transform.Rotate(new Vector3(0, 90, 0) * speed * Time.deltaTime);
	}

}
