using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public GameObject target;
	Vector3 startPosition;
	Vector3 endPosition;
	Vector3 targetPosition;
	const float moveSpeed = .33f;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		endPosition = target.transform.position;
		targetPosition = endPosition;
	}


	void OnCollisionEnter2D(Collision2D collision) {
		Character character = collision.collider.GetComponent<Character>();
		character.transform.SetParent(transform);
	}

	void OnCollisionExit2D(Collision2D collision) {
		Character character = collision.collider.GetComponent<Character>();
		character.transform.SetParent(null);
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, targetPosition) <= Mathf.Epsilon) {
			targetPosition = endPosition == targetPosition ? startPosition : endPosition;
		}
		float step = moveSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
	}
}
