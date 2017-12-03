using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Text timer;
	private const float followerDelay = 0.15f;
	public float timeLimit = 60.0f;

	public bool levelComplete = false;
	public bool gameOver = false;
	public GameObject gameOverCanvas;

	private List<Follower> followers = new List<Follower>();

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Assert.IsNull(instance);
		}
	}

	void Start() {
		timer.text = timeLimit.ToString("F2");
		ScoreManager.instance.Reset();
	}

	void Update() {
		if(!levelComplete) {
			timeLimit -= Time.deltaTime;
			timer.text = timeLimit.ToString("F2");
		}
		UpdateScore();
		if(gameOver) {
			gameOverCanvas.gameObject.SetActive(true);
			if(Input.GetButton("Jump")){
				SceneManager.LoadScene(1);
			}
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

	void UpdateScore() {
		ScoreManager.instance.timerScore = (int)timeLimit;
		ScoreManager.instance.followers = followers.Count;
	}

	public void GameOver(){
		gameOver = true;
	}

}
