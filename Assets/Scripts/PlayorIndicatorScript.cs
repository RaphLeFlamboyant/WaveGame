using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayorIndicatorScript : MonoBehaviour {
    float startTime;
    float opacity = 1;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (opacity > 0)
        {
            opacity = 1 - (Time.time - startTime) / 3;

            if (opacity < 0)
                opacity = 0;

            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, opacity);
        }
	}
}
