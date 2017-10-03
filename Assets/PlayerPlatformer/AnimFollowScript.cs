using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFollowScript : MonoBehaviour {
    public Transform playerPhysicsTransform;
    //public float offsetX, offsetY;

    void Start()
    {
    }

    void Update()
    {
        var fv = playerPhysicsTransform.position;
        this.transform.position = new Vector3(fv.x, fv.y,fv.z);
    }
}
