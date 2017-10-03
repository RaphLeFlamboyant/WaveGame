using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InteractableObject : MonoBehaviour {
	public bool isInPlayerInfluence = false;
	[SerializeField]
	LayerMask lmask;

	[SerializeField]
	public UnityEvent switchStateCallback;

	public void SwitchState()
	{
		switchStateCallback.Invoke ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag.Equals ("PlayerInfluence"))
			isInPlayerInfluence = true;
	}

	void  OnTriggerExit2D(Collider2D coll){
		if (coll.tag.Equals ("PlayerInfluence"))
			isInPlayerInfluence = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			CastRay();
	}

	void CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, lmask);
		if (hit.collider != null && hit.collider == this.GetComponent<Collider2D>() && isInPlayerInfluence)
			SwitchState();
	}   
/*
	void OnMouseDown()
	{
		if (isInPlayerInfluence && Input.GetMouseButtonDown (0))
			SwitchState();
	}*/
}
