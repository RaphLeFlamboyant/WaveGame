using UnityEngine;
using System.Collections;

public class DumbCameraScript : MonoBehaviour {
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null && !transform.position.Equals (target.position)) {
			transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
		}
	}
}
