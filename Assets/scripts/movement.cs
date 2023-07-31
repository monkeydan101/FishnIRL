using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class movement : MonoBehaviour
{


    public Animator myAnim;

    public float moveSpeed = 7f;
    public bool isGrounded = false;

    public Vector3 PlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>(); //grabbing the animator component
        myAnim.Play("Land");
    }

    // Update is called once per frame
    void Update()
    {
        //for jumping
        if(Input.GetKeyDown("space") && isGrounded == true){
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f,13f,0f));
        }

        /*
        if (Input.GetKeyDown("w"))
        {
            gameObject.
        }
        */

    }
}
