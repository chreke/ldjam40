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
		}

		if (Input.GetAxisRaw("Horizontal") != 0) {
			player.Move ((int)Mathf.Sign(Input.GetAxisRaw ("Horizontal")));
		}

	}
}
