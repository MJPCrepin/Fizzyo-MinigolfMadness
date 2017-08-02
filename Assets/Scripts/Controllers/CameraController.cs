using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public PointerController pc;
    private Vector3 offset; // player-cam distance

	void Start ()
    {
        offset = transform.position - player.transform.position;
	}


	public void UpdateDirection()
    {
        var convertedDirection = pc.getDirection() * (float)Math.PI / 180;
        transform.rotation = Quaternion.LookRotation(new Vector3((float)Math.Sin(convertedDirection), -1f, (float)Math.Cos(convertedDirection)));
        transform.position = player.transform.position 
            + new Vector3(offset.z * (float)Math.Sin(convertedDirection), offset.y, offset.z * (float)Math.Cos(convertedDirection));
    }

}
