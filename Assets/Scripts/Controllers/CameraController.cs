using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Main game camera behaviour

    public GameObject player;
    public PointerController pc;
    private Vector3 offset; // player-cam distance

	void Start ()
    { // Calculate player-cam distance
        offset = transform.position - player.transform.position;
	}

	public void UpdateDirection()
    { // Rotate the camera to always look at player, and position it at constant distance (offset)
        var convertedDirection = pc.getDirection() * (float)Math.PI / 180;
        transform.rotation = Quaternion.LookRotation(new Vector3((float)Math.Sin(convertedDirection), -1f, (float)Math.Cos(convertedDirection)));
        transform.position = player.transform.position 
            + new Vector3(offset.z * (float)Math.Sin(convertedDirection), offset.y, offset.z * (float)Math.Cos(convertedDirection)); // yay maths
    }

}
