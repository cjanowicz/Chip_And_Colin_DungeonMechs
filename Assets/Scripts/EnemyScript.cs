﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    /// <summary>
    /// Code for enemy type that moves until it hits a wall in a direction specified.
    /// </summary>
    enum PatrolState { Patrol, Paused, Returning };
    PatrolState currentState = PatrolState.Patrol;
    public Vector2 patrolDirection;
    private Vector2 currentDirection;
    public float patrolDuration = 4;
    private float currentTimer = 0f;
    public float stunDuration = 2;
    private float stunTimer = 0f;
    public float speed;
    public int health;
    Rigidbody2D myRigidbody2D;
    public GameObject explosion;
    private Vector2 origin;
    private float dragDefault;
    public float dragWhenStunned = 1f;
    private Animator myAnim;

    // Use this for initialization
    void Start () {
        if (patrolDirection.magnitude == 0) 
            patrolDirection = Vector2.left;
         else
            patrolDirection.Normalize();

        myRigidbody2D = GetComponent<Rigidbody2D>();
        currentTimer = patrolDuration;
        currentDirection = patrolDirection;
        dragDefault = myRigidbody2D.drag;
        myAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        ////Turn the other way detection here.

        ////Note: Look into state machine behavior 
        if (stunTimer >= 0) {
            stunTimer -= Time.deltaTime;
            if(stunTimer <= 0) {
                UndoStun();
            }
        } else {
            myRigidbody2D.AddForce(currentDirection * speed);
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f) {
                if(currentState == PatrolState.Patrol) {
                    Debug.Log("Paused Start");
                    currentDirection = Vector2.zero;
                    currentState = PatrolState.Paused;
                    patrolDirection *= -1;
                    currentTimer = stunDuration;
                } else if( currentState == PatrolState.Paused) {
                    Debug.Log("Patrol Start");
                    currentState = PatrolState.Patrol;
                    currentDirection = patrolDirection;
                    currentTimer = patrolDuration;
                }
            }
        }

    }

    public void Stun() {
        stunTimer = stunDuration;
        myRigidbody2D.drag = dragWhenStunned;
        myAnim.SetBool("StunnedBool", true);
    }
    public void UndoStun() {
        myRigidbody2D.drag = dragDefault;
        myAnim.SetBool("StunnedBool", false);
    }

    public void TakeDamage(int damage) {

        Debug.Log("TakeDamage Message Received");
        Stun();
        origin = transform.position;
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    void Die() {
        //Call Explosion here
        Instantiate(explosion, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
