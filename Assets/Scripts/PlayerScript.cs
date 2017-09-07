using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Camera mainCamera;
    public float speed;
    private Rigidbody2D myRigidbody2D;
    public Vector2 inputAxes;
    public Animator anim;
    Vector3 mousePosition;
    Vector2 mousePosDiff;
    public Transform swordRoot;

    // Use this for initialization
    void Start () {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        ////Player Facing mouse code
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosDiff = transform.position - mousePosition;
        if (mousePosDiff.magnitude > 1) {
            //If Vertical is bigger than X
            if(Mathf.Abs(mousePosDiff.y) > Mathf.Abs(mousePosDiff.x)) {
                if(mousePosDiff.y < 0)
                    anim.SetTrigger("FacingUp");
                else
                    anim.SetTrigger("FacingDown");
            } else {
                if (mousePosDiff.x < 0)
                    anim.SetTrigger("FacingRight");
                else
                    anim.SetTrigger("FacingLeft");
            }
        }
        ////Mouse facing rotation code;
        //Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        //transform.rotation = rot;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //myRigidbody2D.angularVelocity = 0;

        ////Movement Code
        inputAxes.Set(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        myRigidbody2D.AddForce(inputAxes * speed);

        ////Attack Code;
        if(Input.GetButton("Fire1")) {
            ////Note: Had to reverse Vector3.forward and mousePosDiff for some reason, not sure why.
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, mousePosDiff * -1);
            swordRoot.rotation = rot;
            //swordRoot.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            //myRigidbody2D.angularVelocity = 0;
            anim.SetTrigger("Attack");
        }
    }
}
