using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public enum WaveType
    {
        Electric,
        Water,
        Ground,
        Fire
    }

    public WaveType waveType;
    public float damages = 1;
    public string inputActivation;

    private FoePlayerController foeController;

    public float scaleTargeted = 10;
    public float secondsToReachTarget = 2;

    private float timeGrowing = 0f;

	// Use this for initialization
    void Start () {
        foeController = GameObject.Find("FoePlayerController").GetComponentInChildren<FoePlayerController>();

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
	}
	
	// Update is called once per frame
    void Update () {
        if (timeGrowing != 0)
        {
            var deltaTime = Time.deltaTime;

            var transform = GetComponent<Transform>();

            timeGrowing += deltaTime;
            if (timeGrowing >= secondsToReachTarget)
                timeGrowing = 0;

            var scale = 1 + timeGrowing / secondsToReachTarget * (scaleTargeted - 1);

            transform.localScale = new Vector3(scale, scale);
        }
    }

    public void StartGrowing(bool specialHit = false) {
        if (specialHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.9f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        secondsToReachTarget = specialHit ? 1.5f : 2f;

        var effect = GetComponent<AudioSource>();
        effect.Play();

        timeGrowing += 0.001f;
	}

    public bool Growing 
    {
        get
        {
            return timeGrowing > 0;
        }
    }
}
