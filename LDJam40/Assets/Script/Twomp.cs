using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twomp : MonoBehaviour {

	Rigidbody2D rb;
	public BoxCollider2D dropZone;
	Vector3 startPosition;
	bool hasFallen;
	public bool isRising;

	float waittime = 2f;
	float riseSpeed = 1.5f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		startPosition = transform.position;
		hasFallen = false;
		isRising = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("wat");
		if(isRising) {
			Rise();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Follower") {
			Fall();
		}
	}

	void Fall(){
		hasFallen = true;
		rb.gravityScale = 1;
		dropZone.gameObject.SetActive(false);
		StartCoroutine(CountDownToRise());
	}

	void Rise(){
		rb.gravityScale = 0;
		Debug.Log("Rising!");
		float step = riseSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
		if(transform.position == startPosition) {
			Reset();
		}
	}

	void Reset(){
		isRising = false;
		hasFallen = false;
		dropZone.gameObject.SetActive(true);
	}

	IEnumerator CountDownToRise(){
		yield return new WaitForSeconds(waittime);
		isRising = true;
		Rise();
	}
}
