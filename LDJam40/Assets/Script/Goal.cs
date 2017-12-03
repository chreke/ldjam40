using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Goal : MonoBehaviour {

    bool hasWon = false;
    public int goalScore = 2000;
    public float timeBeforeScoreLoad = 2f;
    
    public ParticleSystem goldenShower;
	AudioSource aSource;
	public AudioClip[] coinSound;

	// Use this for initialization
	void Start () {
		aSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(!hasWon) {
			if(other.gameObject.GetComponent<PlayerController>()) {
				ScoreManager.instance.treasure = 2000;
				hasWon = true;
				GameManager.instance.levelComplete = true;
				StartCoroutine(Win(timeBeforeScoreLoad));
                GetComponent<Animator>().SetTrigger("reward");
                goldenShower.Play();
				for(int i = 0; i < 25; i++) {
					StartCoroutine(PlayCoinSound(0.2f*i));
				}
            }
		}
	}

	IEnumerator Win(float timeout){
		yield return new WaitForSeconds(timeout);
		SceneManager.LoadScene(2);
	}

	IEnumerator PlayCoinSound(float delay){
		yield return new WaitForSeconds(delay * Random.Range(0.95f, 1.05f));
		aSource.PlayOneShot(coinSound[Random.Range(0,coinSound.Length)]);
	}
}
