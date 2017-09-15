using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public int damage = 1;
    public float pushForce = 70;
    private Vector2 pushVector;
    private Rigidbody2D otherRigidbody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTrigger2D Called");
        if(other.tag == "Enemy") {

            pushVector = other.transform.position - this.transform.parent.position;
            pushVector = pushVector.normalized * pushForce;
            otherRigidbody = other.GetComponent<Rigidbody2D>();
            otherRigidbody.AddForce(pushVector);
            other.SendMessage("TakeDamage", damage);
        }
    }
}
