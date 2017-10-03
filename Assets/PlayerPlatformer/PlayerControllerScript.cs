using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PlayerControllerScript : MonoBehaviour {
	public FeetsScript feets;
	public GameInputManager inputManager;
	public float jumpForce = 8;
	public float jumpMaintainForce = 8;
	public float moveForce = 8;
	public float minVelocityX = 0.5f;
    public float healItem = 2;
    public float timeToRepair = 2;

    public AudioClip jumpClip;
    public AudioClip repair1Clip;
    public AudioClip repair2Clip;
    public AudioClip repair3Clip;

    private bool lastFrameMovingHorizontal = false;
    public bool isRepairing = false;
    private float timeRepairing = 0;

	IDictionary<string, bool> lastInputValue;
    PlayerData playerData;

	// Use this for initialization
    void Start () {
        playerData = GetComponentInParent<PlayerData> ();

		//GetComponent<Rigidbody2D> ().freezeRotation = true;
		lastInputValue = new Dictionary<string, bool> (){
			{"A", false},
			{"B", false},
			{"X", false},
			{"Y", false},
			{"Left", false},
			{"Right", false}
		};
	}
	
	// Update is called once per frame
    void Update () {
        HandleItemRepair();
        HandleCharacterMoving();

        var keys = lastInputValue.Keys.ToArray();
		foreach (var key in keys)
		{
			lastInputValue[key] = inputManager.IsPressed(key, playerData.playerID);
        }
	}

    private void HandleItemRepair()
    {
        var items = GetComponentInParent<PlayerData>().GetItems();
        var effect = GetComponent<AudioSource>();

        if (inputManager.IsPressed("B", playerData.playerID) && !lastInputValue["B"])
        {
            if (items.Any(i => i.GetComponent<ItemBehaviorScript>().hp < i.GetComponent<ItemBehaviorScript>().MaxHP) && !isRepairing)
            {
                isRepairing = true;
                timeRepairing = 0;


                effect.Stop();
                effect.clip = (Time.deltaTime * 1000 % 3) == 2 ? repair1Clip : (Time.deltaTime * 1000 % 3) == 1 ? repair2Clip : repair3Clip;
                effect.volume = 1;
                effect.Play();

            }            
        }
        else
        {
            if (timeRepairing > timeToRepair)
            {
                isRepairing = false;

                effect.Stop();

                timeRepairing = 0;
                foreach (var item in items)
                {
                    var script = item.GetComponent<ItemBehaviorScript>();
                    script.Repair(healItem);
                }
            }
            else if (isRepairing)
                timeRepairing += Time.deltaTime;
        }
    }

    private void HandleCharacterMoving()
    {
        var rgb = GetComponent<Rigidbody2D> ();

        if (isRepairing && rgb.velocity.x != 0)
        {
            rgb.velocity = new Vector2(0, rgb.velocity.y);
            rgb.angularVelocity = 0;
        }

        if (!isRepairing && inputManager.IsPressed("A", playerData.playerID) && !lastInputValue["A"])
        {
            if (feets.isGrounded)
            {
                var effect = GetComponent<AudioSource>();
                effect.Stop();
                effect.clip = jumpClip;
                effect.volume = 0.5f;
                effect.Play();

                rgb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                feets.isJumping = true;
            }
        }

        feets.isJumping = !isRepairing && rgb.velocity.y > 0;

        if (inputManager.IsPressed("A", playerData.playerID) && feets.isJumping)
        {
            rgb.AddForce(Vector2.up * jumpMaintainForce, ForceMode2D.Force);
        }

        if (!isRepairing && inputManager.IsPressed("Left", playerData.playerID))
        {
            rgb.AddForce(Vector2.left * moveForce);
            lastFrameMovingHorizontal = true;

            //cap speed
            if (rgb.velocity.x < -3.7)
                rgb.velocity = new Vector2(-3.7f, rgb.velocity.y);
        }
        else if (!isRepairing && inputManager.IsPressed("Right", playerData.playerID))
        {           
            rgb.AddForce(Vector2.right * moveForce);
            lastFrameMovingHorizontal = true;

            //cap speed
            if (rgb.velocity.x > 3.7)
                rgb.velocity = new Vector2(3.7f, rgb.velocity.y);
        }
        else if (lastFrameMovingHorizontal)
        {
            if (feets.isGrounded)
            {
                rgb.velocity = new Vector2(0, rgb.velocity.y);
                rgb.angularVelocity = 0;
                lastFrameMovingHorizontal = false;
            }
        }
    }
}
