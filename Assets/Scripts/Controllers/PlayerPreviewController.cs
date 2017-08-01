using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreviewController : MonoBehaviour {

	private void Update ()
    {
        transform.position += Vector3.back * 3 * Time.deltaTime;
	}
}
