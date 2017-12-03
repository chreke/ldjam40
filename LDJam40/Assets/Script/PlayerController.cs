using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour {

	private Character player;
	public int numberOfFollowers = 20;
	public Follower followerPrefab;

	// Use this for initialization
	void Start() {
		player = GetComponent<Character> ();
		InitFollowers();
	}
	
	// Update is called once per frame
	void Update() {
		if(!GameManager.instance.levelComplete) {
			Move();
		}
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

	void InitFollowers() {
		for(int i = 0; i < numberOfFollowers; i++) {
			Follower newFollower = Instantiate(followerPrefab, new Vector3(player.transform.position.x + Random.Range(-0.05f, 0.05f), player.transform.position.y + Random.Range(0f, 0.05f), player.transform.position.z), Quaternion.identity);
		}
	}
}
