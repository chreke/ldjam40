using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	float speed = 3.5f;
	float jumpForce = 350f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move() {
		float moveDirection = 0;
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			moveDirection = Mathf.Sign (Input.GetAxis ("Horizontal"));
		}
		transform.position += Vector3.right * moveDirection * Time.deltaTime * speed;
	}
}
