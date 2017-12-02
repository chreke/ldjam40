using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour {

	float moveSpeed = 3.5f;
	float jumpForce = 350f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Move(int direction) {
		transform.position += Vector3.right * direction * Time.deltaTime * moveSpeed;
	}

	public void Jump() {
		rb.AddForce (Vector2.up * jumpForce);
	}
}
