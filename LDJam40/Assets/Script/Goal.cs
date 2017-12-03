using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	bool hasWon = false;
	public float timeBeforeScoreLoad = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!hasWon) {
			if(other.gameObject.GetComponent<PlayerController>()) {
				hasWon = true;
				StartCoroutine(Win(timeBeforeScoreLoad));
			}
		}
	}

	IEnumerator Win(float timeout){
		yield return new WaitForSeconds(timeout);
		SceneManager.LoadScene(2);
	}
}
