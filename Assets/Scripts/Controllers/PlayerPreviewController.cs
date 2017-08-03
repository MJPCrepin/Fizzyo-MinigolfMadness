using UnityEngine;

public class PlayerPreviewController : MonoBehaviour {

	private void Update ()
    {
        transform.position += Vector3.forward * 3 * Time.deltaTime;
	}
}
