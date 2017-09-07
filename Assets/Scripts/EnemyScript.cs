using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    /// <summary>
    /// Code for enemy type that moves until it hits a wall in a direction specified.
    /// </summary>

    public Vector2 patrolDirection;
    private float currentTimer = 0f;
    public const float stunTime = 2;
    public float speed;
    public int health;
    Rigidbody2D myRigidbody2D;

    // Use this for initialization
    void Start () {
        if (patrolDirection.magnitude == 0) 
            patrolDirection = Vector2.left;
         else
            patrolDirection.Normalize();

        myRigidbody2D = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        myRigidbody2D.AddForce(patrolDirection * speed);
        ////Turn the other way detection here.
    }

    public void TakeDamage(int damage) {

        Debug.Log("TakeDamage Message Received");
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    void Die() {
        //Call Explosion here
        this.gameObject.SetActive(false);
    }
}
