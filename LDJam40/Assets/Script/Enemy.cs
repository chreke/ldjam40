using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {


	// Use this for initialization
	void Start () {
				
	}


	void OnTriggerEnter2D(Collider2D other) {

		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Follower") {
			Character character = other.gameObject.GetComponent<Character>();
			character.Kill();
		}

	}


	// Update is called once per frame
	void Update () {

	}
}