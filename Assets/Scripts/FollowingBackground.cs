using UnityEngine;
using System.Collections;

public class FollowingBackground : MonoBehaviour {
	public Transform toTrack;
	public float moveRatio = 1f / 20f;
	private Vector2 lastFramePosition;

	// Use this for initialization
	void Start () {
		lastFramePosition = new Vector2(toTrack.position.x, toTrack.position.y);
	}

	// Update is called once per frame
	void Update () {
		Vector2 delta = ((Vector2) toTrack.position) - lastFramePosition;

		transform.Translate (new Vector2(delta.x * (1 + moveRatio), delta.y * (1 - moveRatio)));

		lastFramePosition = new Vector2(toTrack.position.x, toTrack.position.y);
	}
}
