using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScorePresentation : MonoBehaviour {

	public int timerScore = 30;
	public int treasureScore = 5000;
	public int followers = 2;

	public Text timerText;
	public Text treasureText;
	public Text followerText;
	public Text splitText;
	public Text scoreText;

	public Text playAgainText;

	bool canPlayAgain = false;

	// Use this for initialization
	void Start () {
		timerScore = ScoreManager.instance.timerScore;
		followers = ScoreManager.instance.followers;
		treasureScore = ScoreManager.instance.treasure;

		treasureText.text = "Treasure:";
		timerText.text = "Time Bonus:";
		followerText.text = "Survivors:";
		scoreText.text = "Your Share:";
		StartCoroutine(ShowScore());
	}
	
	// Update is called once per frame
	void Update () {
		if(canPlayAgain) {
			if(Input.GetButton("Jump")) {
				SceneManager.LoadScene(1);
			}
		}
	}

	IEnumerator ShowScore(){
		yield return new WaitForSeconds(1f);
		treasureText.text = "Treasure:\t" + treasureScore;
		yield return new WaitForSeconds(1f);
		timerText.text = "Time Bonus:\tx" + GetTimeMultiplier();
		yield return new WaitForSeconds(1f);
		followerText.text = "Survivors:\t" + (followers + 1);
		yield return new WaitForSeconds(1f);
		scoreText.text = "Your Share:\t" + CalculateScore();
		yield return new WaitForSeconds(1f);
		canPlayAgain = true;
		playAgainText.text = "Press [JUMP] to play again.";
	}

	float GetTimeMultiplier() {
		return 1 + (float)timerScore / 100;
	}

	int CalculateScore() {
		float timeMultiplier = GetTimeMultiplier();

		float totalScore = (treasureScore * timeMultiplier) / (followers + 1);
		//Debug.Log(totalScore);
		return (int)totalScore;
	}
}
