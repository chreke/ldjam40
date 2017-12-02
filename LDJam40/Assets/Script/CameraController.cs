using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	GameObject player;
	float smoothSpeed = 0.125f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
	}
}
