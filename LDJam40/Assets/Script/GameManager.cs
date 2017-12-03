using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public Text timer;
	public Text teamSize;
	private const float followerDelay = 0.15f;
	public float timeLimit = 10.0f;

	public bool levelComplete = false;
	public bool gameOver = false;
	public GameObject gameOverCanvas;

	public PlayerController player;

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
            float oldLimit = timeLimit;
            int current = (int)timeLimit;
			timeLimit = Mathf.Max(timeLimit - Time.deltaTime, 0);
            int next = (int)timeLimit;

            if(current != next && next < 10 && next >= 0)
            {
                Camera.main.GetComponent<AudioSource>().Play();
            }

			timer.text = timeLimit.ToString("F2");
			teamSize.text = "Teamsize: " + (followers.Count + 1);

            if (timeLimit <= 0 && oldLimit > 0)
            {
                timeLimit = 0;
                timer.text = timeLimit.ToString("F2");
                player.GetComponent<Character>().Kill();
                KillAll();
            }

        }
		UpdateScore();
		if(gameOver) {
			gameOverCanvas.gameObject.SetActive(true);
			if(Input.GetButton("Jump")){
				SceneManager.LoadScene(1);
			}
		}
	}

    private IEnumerator Kasplode(float jumpDelay, float delay, Follower f)
    {
        yield return new WaitForSeconds(jumpDelay);
        f.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f, 100f), Random.Range(100f, 300f)));
        yield return new WaitForSeconds(delay);
        f.GetComponent<Character>().Kill();
    }

    private void KillAll()
    {
        List<Follower> tmpFollowers = new List<Follower>(followers);

 
        foreach (Follower f in tmpFollowers)
        {
            StartCoroutine(Kasplode(Random.RandomRange(0.01f, 0.5f), Random.RandomRange(0.1f, 0.3f), f));
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
