using UnityEngine;

public class PlayerPreviewController : MonoBehaviour {

    // Moves player in menu screen so trail can be previewed

	private void Update ()
    {
        transform.position += Vector3.forward * 3 * Time.deltaTime;
	}
}
