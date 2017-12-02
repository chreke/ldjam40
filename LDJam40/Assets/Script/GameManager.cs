using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private List<Follower> followers = new List<>();

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Assert.IsNull(instance);
		}
	}

	void recordChangeDirection(int direction) {
		float currentTime = Time.time;
		foreach (Follower f in followers) {
			f.pushChangeDirection(currentTime, direction);
		}
	}

	void recordJump() {
		float currentTime = Time.time;
		foreach (Follower f in followers) {
			f.pushJump(currentTime);
		}
	}

	void registerFollower(Follower follower) {
		followers.Add(follower);
	}
}
