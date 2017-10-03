using UnityEngine;
using System.Collections;

public class PlayerStatesUpdaterScript : MonoBehaviour {	
	public Rigidbody2D platformerRgb;
	public FeetsScript feets;
    public PlayerControllerScript playerCtr;
	private bool isJumping = false;
    private bool isRunning = false;
    private bool isRepairing = false;

	// Update is called once per frame
	void Update () {
		var animator = GetComponent<Animator> ();
		var spr = GetComponent<SpriteRenderer> ();

        isRepairing = playerCtr.isRepairing;
        animator.SetBool ("isRepairing", isRepairing);

		if (platformerRgb.velocity.x > 0.001 || platformerRgb.velocity.x < -0.001) {
			if (!isRunning) {
				animator.SetBool ("isWalking", true);
				isRunning = true;
			}

			if (platformerRgb.velocity.x > 0.1)
				spr.flipX = false;
			else
				spr.flipX = true;
		}
		else {
			if (isRunning) {
				animator.SetBool ("isWalking", false);
					isRunning = false;
				}
		}

		if (platformerRgb.velocity.y > 0 && !feets.isGrounded) {
			if (!isJumping) {
				animator.SetBool ("isJumping", true);
				isJumping = true;
			}
		}
		else {
			if (isJumping) {
				animator.SetBool ("isJumping", false);
				isJumping = false;
			}
		}
	}
}
