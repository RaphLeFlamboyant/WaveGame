using UnityEngine;
using System.Linq;
using System.Collections;

public class FeetsScript : MonoBehaviour {
	public bool isGrounded;
	public bool isJumping;
	private int nbFrames;
    public int groundHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*var coll = GetComponent<Collider2D> ();
		var hit = Physics2D.Raycast(new Vector2(coll.bounds.min.x, coll.bounds.min.y - 0.001f), Vector2.down, coll.bounds.size.y / 4)
			|| Physics2D.Raycast(new Vector2(coll.bounds.center.x, coll.bounds.min.y - 0.001f), Vector2.down, coll.bounds.size.y / 4)
			|| Physics2D.Raycast(new Vector2(coll.bounds.max.x, coll.bounds.min.y - 0.001f), Vector2.down, coll.bounds.size.y / 4);
		isGrounded = hit;*/// && !hit.collider.isTrigger;
	}

	void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Block") && !coll.isTrigger){
            groundHit++;
            isGrounded =  !isJumping;
		}
		/*
		isGrounded = numberPlatformsTouch > 0;

		/*Transform tf = transform.root.GetComponentsInChildren<Transform>().First(c => c.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerTronche")));
		var anim = tf.GetComponent<Animator> ();
		if (!isGrounded) {
			anim.SetBool ("isInTheAirs", true);
		}
		else
			anim.SetBool ("isInTheAirs", false);*/
	}

    void  OnTriggerExit2D(Collider2D coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer("Block") && !coll.isTrigger){
            //|| (coll.gameObject.layer == LayerMask.NameToLayer("PlatformOneWay") && )){
            groundHit--;
            isGrounded = groundHit > 0 && !isJumping;

            if (groundHit < 0)
                throw new UnityException("Ground hit < 0 !!");
        }
    }
	
	void OnTriggerStay2D(Collider2D coll){
        if (!isJumping && groundHit > 0)
            isGrounded = true;
	}
    /*
	void  OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("Block") && !coll.isTrigger) {
			numberPlatformsTouch--;
		}
		
		isGrounded = numberPlatformsTouch > 0;

		//Transform tf = transform.root.GetComponentsInChildren<Transform>().First(c => c.gameObject.layer.Equals(LayerMask.NameToLayer("PlayerTronche")));
		//var anim = tf.GetComponent<Animator> ();
		//if (!isGrounded) {
			//anim.SetBool ("isInTheAirs", true);
		//}
		//else
			//anim.SetBool ("isInTheAirs", false);
	}*/
}
