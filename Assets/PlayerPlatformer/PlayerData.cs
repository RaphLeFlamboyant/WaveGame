using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public int playerID;

    List<Collider2D> items;

	// Use this for initialization
	void Start () {
        playerID = GameObject.Find("Game Controller").GetComponent<EnvironmentData>().playerDefending;


        items = new List<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Collider2D> GetItems()
    {
        return items;
    }
}
