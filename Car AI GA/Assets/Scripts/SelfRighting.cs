using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRighting : MonoBehaviour {

    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float velocityThreshold = 1f;

    private float lastOKTime;
    private Rigidbody rigidBody;

	// Use this for initialization
	private void Start () {
        rigidBody = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	private void Update () {
		if (transform.up.y > 0f || rigidBody.velocity.magnitude > velocityThreshold)
        {
            lastOKTime = Time.time;
        }
        if (Time.time > lastOKTime + waitTime)
        {
            RightCar();
        }
	}

    private void RightCar()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}
