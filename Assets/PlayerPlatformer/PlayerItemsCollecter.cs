using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsCollecter : MonoBehaviour {
    private PlayerData playerData;

	// Use this for initialization
	void Start () {
        playerData = GetComponentInParent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.layer == LayerMask.NameToLayer ("Item")){
            playerData.GetItems().Add(coll);
            Debug.Log("Add item, nb : " + playerData.GetItems().Count);
        }
    }

    void  OnTriggerExit2D(Collider2D coll){
        if (coll.gameObject.layer == LayerMask.NameToLayer("Item")){
            playerData.GetItems().Remove(coll);
            Debug.Log("Remove item, nb : " + playerData.GetItems().Count);
        }
    }
}
