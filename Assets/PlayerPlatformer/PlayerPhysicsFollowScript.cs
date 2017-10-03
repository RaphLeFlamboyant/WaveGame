using UnityEngine;
using System.Collections;

public class PlayerPhysicsFollowScript : MonoBehaviour {
	public Transform playerPhysicsTransform;
	public float offsetX, offsetY;

	void Start()
	{
	}

	void Update()
	{
		GetComponent<Rigidbody2D> ().MovePosition (new Vector2 (playerPhysicsTransform.position.x + offsetX, playerPhysicsTransform.position.y));// - offsetY));
		//transform.Translate(new Vector3(playerPhysicsTransform.position.x + offsetX - transform.position.x, playerPhysicsTransform.position.y - offsetY - transform.position.y, 0));
		//transform.position = new Vector2(playerPhysicsTransform.position.x + offsetX, playerPhysicsTransform.position.y - offsetY);
	}
}
