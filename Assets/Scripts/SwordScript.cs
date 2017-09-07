using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public int damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTrigger2D Called");
        if(other.tag == "Enemy") {
            other.SendMessage("TakeDamage", damage);
            Debug.Log("TakeDamage Message Sent");
        }
    }
}
