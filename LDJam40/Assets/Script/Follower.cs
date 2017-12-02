using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour, CharacterListener {

	public float timeOffset = 0f;
	private Character follower;

	private struct Jump {
		public float time;

		public Jump(float time) {
			this.time = time;
		}
	}

	private struct ChangeDirection {
		public float time;
		public int direction;

		public ChangeDirection(float time, int direction) {
			this.time = time;
			this.direction = direction;
		}
	}

	private Queue<Jump> jumps = new Queue<Jump>();
	private Queue<ChangeDirection> directionChanges = new Queue<ChangeDirection>();

	// Use this for initialization
	void Start () {
		follower = GetComponent<Character>();
		follower.AddListener(this);
		GameManager.instance.RegisterFollower(this);
	}

	public void setTimeOffset(float offset) {
		this.timeOffset = offset;
	}

	public void pushJump(float time) {
		jumps.Enqueue(new Jump(time));
	}

	public void pushChangeDirection(float time, int direction) {
		directionChanges.Enqueue(new ChangeDirection(time, direction));
	}

	public void onKill(Character character) {
		GameManager.instance.UnregisterFollower(this);
	}
	
	// Update is called once per frame
	void Update () {
		float currentTime = Time.time;
		
		if (jumps.Count > 0 && jumps.Peek().time + timeOffset <= currentTime) {
			follower.Jump();
			jumps.Dequeue();
		}
		if (directionChanges.Count > 0 && directionChanges.Peek().time + timeOffset <= currentTime) {
			int direction = directionChanges.Dequeue().direction;
			follower.ChangeDirection(direction);
		}
	}
}
