using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoving : MonoBehaviour {
    public Transform cloudSpawn;
    public Transform cloudLimit;
    public float scaleFactor = 0;
    public float positionYFactor = 0;
    public float lateralSpeed = 0;

    private float originalY;
    private float originalX;

    private Rigidbody2D rgd;

	// Use this for initialization
	void Start () {
        rgd = GetComponent<Rigidbody2D>();

        originalY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        var deltaX = Time.deltaTime * lateralSpeed;
        var deltaY = Mathf.Cos(Time.time * Mathf.PI) * positionYFactor;
        var deltaScale = Mathf.Cos((Time.time + 0.5f) * Mathf.PI) * scaleFactor;

        var tns = this.transform;

        if (tns.position.x < cloudLimit.position.x)
            tns.position = new Vector3(cloudSpawn.position.x, tns.position.y, tns.position.z);

        tns.position = new Vector3(tns.position.x - deltaX, originalY + deltaY, tns.position.z);
        tns.localScale = new Vector3(1 + deltaScale, 1 + deltaScale, 1);

    }
}
