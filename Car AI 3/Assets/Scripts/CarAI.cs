using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour {

    public Transform path;
    public float currentSpeed = 0f;
    public float maxSpeed = 1f;
    public float maxMotorTorque = 100f;
    public float maxBrakingTorque = 200f;
    public float maxSteerAngle = 35f;
    public Vector3 centerOfMass;

    public WheelCollider wheelfl;
    public WheelCollider wheelfr;
    public WheelCollider wheelrr;
    public WheelCollider wheelrl;

    private List<Transform> nodes;
    private int current = 0;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < pathTransforms.Length; ++i)
            if(pathTransforms[i] != path.transform)
                nodes.Add(pathTransforms[i]);
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
        Steer();
        Drive();
        Next();
	}

    private void Steer()
    {
        Vector3 relative = this.transform.InverseTransformPoint(nodes[0].position);
        float steer = relative.x / relative.magnitude;
        wheelfl.steerAngle = steer;
        wheelfr.steerAngle = steer;
        Debug.DrawLine(this.transform.position, nodes[current].position);
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelfl.radius * wheelfl.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed)
        {
            wheelrl.motorTorque = maxMotorTorque;
            wheelrr.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelrl.motorTorque = 0;
            wheelrr.motorTorque = 0;
        }
    }

    private void Next()
    {
        if (Vector3.Distance(this.transform.position, nodes[current].position) < 5f)
        {
            if (current == nodes.Count - 1)
            {
                current = 0;
            }
            else
            {
                current++;
            }
        }
    }
}
