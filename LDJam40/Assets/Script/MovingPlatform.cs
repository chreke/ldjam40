using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float moveSteps;
	Vector3 startPosition;
	Vector3 endPosition;
	Vector3 targetPosition;
	const float moveSpeed = .1f;




	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		endPosition = transform.position + new Vector3(moveSteps, 0f, 0f);
		targetPosition = endPosition;
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
