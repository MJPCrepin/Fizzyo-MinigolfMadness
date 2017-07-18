using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public PointerController pc;

    // Distance between camera and player
    private Vector3 offset;

	void Start ()
    {
        offset = transform.position - player.transform.position;
	}


	public void UpdateDirection()
    {
        //transform.position = player.transform.position + offset;
        var convertedDirection = pc.getDirection() * (float)Math.PI / 180;
        transform.rotation = Quaternion.LookRotation(new Vector3((float)Math.Sin(convertedDirection), -1f, (float)Math.Cos(convertedDirection)));
        transform.position = player.transform.position 
            + new Vector3(offset.z * (float)Math.Sin(convertedDirection), offset.y, offset.z * (float)Math.Cos(convertedDirection));
    }

}
