using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	public float timeOffset;
	private Character follower;

	private struct Jump {
		public float time;
	}

	private struct ChangeDirection {
		public float time;
		public int direction;
	}

	private Queue<Jump> jumps = Queue<Jump>();
	private Queue<ChangeDirection> directionChanges = Queue<ChangeDirection>();

	// Use this for initialization
	void Start () {
		follower = GetComponent<Character> ();
	}

	void pushJump(float time) {
		jumps.Enqueue(new Jump(time));
	}

	void pushChangeDirection(float time, int direction) {
		directionChanges.Enqueue(new ChangeDirection(time, direction));
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		if (jumps.Count > 0 && jumps.Peek().time >= currentTime) {
			follower.Jump();
			jumps.Dequeue();
		}
		if (directionChanges.Count > 0 && directionChanges.Peek().time >= currentTime) {
			int direction = directionChanges.Dequeue().direction;
			follower.ChangeDirection(direction);
		}
	}
}
