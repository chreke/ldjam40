using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour {
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	private const float burnTime = 1f;
	private const float idleTime = 3f;
	private bool isBurning = false;

	void Awake() {
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		
	}

	private IEnumerator Burn() {
		isBurning = true;
		spriteRenderer.enabled = true;
		boxCollider.enabled = true;
		yield return new WaitForSeconds(burnTime);
		spriteRenderer.enabled = false;
		boxCollider.enabled = false;
		yield return new WaitForSeconds(idleTime);
		isBurning = false;
	}

	// Update is called once per frame
	void Update () {
		if (!isBurning) {
			Burn();
		}
	}
}
