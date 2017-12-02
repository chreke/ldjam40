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
		transform.position += Vector3.right * Input.GetAxisRaw ("Horizontal") * Time.deltaTime * speed;
		if (Input.GetButtonDown ("Jump")) {
			Jump ();
		}		
	}

	void Jump() {
		rb.AddForce (Vector2.up * jumpForce);
	}
}
