using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

	private Character player;

	// Use this for initialization
	void Start() {
		player = GetComponent<Character> ();
	}
	
	// Update is called once per frame
	void Update() {
		Move ();
	}

	void Move() {

		if (Input.GetButtonDown("Jump")) {
			player.Jump();
			GameManager.instance.RecordJump();
		}

		int direction = 0;
		if (Input.GetAxisRaw("Horizontal") != 0) {
			direction = (int)Mathf.Sign(Input.GetAxisRaw("Horizontal"));
		}
		if (player.GetDirection() != direction) {
			GameManager.instance.RecordChangeDirection(direction);
			player.ChangeDirection(direction);
		}
	}
}
