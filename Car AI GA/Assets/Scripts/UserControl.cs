using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour {

    public float force = 2f;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W)) rb.AddForce(transform.forward * force);
        if (Input.GetKey(KeyCode.A)) rb.AddTorque(transform.up * -force);
	}
}
