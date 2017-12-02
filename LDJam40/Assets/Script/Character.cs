using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour {

	public static class MoveDirection {
		public const int left = -1;
		public const int right = 1;
		public const int none = 0;
	}

	float maxSpeed = 3f;
	float moveSpeed = 100f;
	float dampAmount = 0.15f;
	float jumpForce = 350f;
	int direction = MoveDirection.none;

	private Rigidbody2D rb;
	public bool isGrounded = true;

	int numberOfRays = 5;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfGrounded();
	}

	public void ChangeDirection(int direction) {
		this.direction = direction;
	}

	public void Move(int direction) {
		if(direction != MoveDirection.none) {
			if(direction * rb.velocity.x < maxSpeed) {
				rb.AddForce(Vector2.right * direction * moveSpeed);
			}

			if(Mathf.Abs(rb.velocity.x) > maxSpeed) {
				rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
			}
		} else {
			rb.velocity = new Vector2(rb.velocity.x * (1 - dampAmount), rb.velocity.y);
		}
	}

	public void Jump() {
		if(isGrounded) {
			rb.AddForce(Vector2.up * jumpForce);
		}
	}

	void CheckIfGrounded(){
		isGrounded = false;
		float raySpacing = transform.localScale.x / (numberOfRays - 1);
		Vector3 bottomLeft = new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2);

		for(int i = 0; i < numberOfRays; i++){
			Vector3 startCastFrom = new Vector3(bottomLeft.x + raySpacing * i, bottomLeft.y);
			Vector3 endCastAt = new Vector3(bottomLeft.x + raySpacing * i, bottomLeft.y - 0.05f);

			if(Physics2D.Linecast(startCastFrom, endCastAt, 1 << LayerMask.NameToLayer("Ground"))){
				isGrounded = true;
			}
		}
	}

	void FixedUpdate() {
		Move(direction);
	}
}
