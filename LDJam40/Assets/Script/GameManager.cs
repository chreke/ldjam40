using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private const float followerDelay = 0.20f;

	private List<Follower> followers = new List<Follower>();

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Assert.IsNull(instance);
		}
	}

	public void RecordChangeDirection(int direction) {
		float currentTime = Time.time;
		foreach (Follower f in followers) {
			f.pushChangeDirection(currentTime, direction);
		}
	}

	public void RecordJump() {
		float currentTime = Time.time;
		foreach (Follower f in followers) {
			f.pushJump(currentTime);
		}
	}

	public void RegisterFollower(Follower follower) {
		followers.Add(follower);
		CalculateFollowerOffsets();
	}

	public void UnregisterFollower(Follower follower) {
		followers.Remove(follower);
		CalculateFollowerOffsets();
	}

	void CalculateFollowerOffsets() {
		for (int i = 0; i < followers.Count; i++) {
			followers[i].setTimeOffset((i + 1) * followerDelay);
		}
	}
}
