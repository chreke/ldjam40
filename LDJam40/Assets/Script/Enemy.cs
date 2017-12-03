using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {


	// Use this for initialization
	void Start () {
				
	}


	void OnTriggerEnter2D(Collider2D other) {
		Character character = other.gameObject.GetComponent<Character>();
        if (character != null) {
            character.Kill();
        }
	}


	// Update is called once per frame
	void Update () {

	}
}