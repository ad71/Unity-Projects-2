using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRighting : MonoBehaviour {

    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float velocityThreshold = 1f;

    private float lastOKTime;
    private Rigidbody rigidBody;
    private int deadC = 0;

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
        if (rigidBody.velocity.magnitude < 1f)
        {
            deadC++;
        }
        if (deadC > 150)
        {
            RightCar();
            deadC = 0;
        }
        Debug.Log("DeadC: " + deadC);
	}

    private void RightCar()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }
}
