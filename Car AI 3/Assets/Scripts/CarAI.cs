using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 35f;
    public float currentSpeed = 0f;
    public float maxSpeed = 150f;
    public float maxMotorTorque = 300f;
    public float maxBrakeTorque = 400f;

    [Header("Wheel Colliders")]
    public WheelCollider wheelfl;
    public WheelCollider wheelfr;
    public WheelCollider wheelrl;
    public WheelCollider wheelrr;

    private List<Transform> nodes;
    private int current = 7;
	// Use this for initialization
	void Start () {
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
        float steer = relative.x / relative.magnitude * maxSteerAngle;
        wheelfl.steerAngle = steer;
        wheelfr.steerAngle = steer;
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
            wheelrr.motorTorque = 0;
            wheelrl.motorTorque = 0;
        }
    }

    private void Next()
    {
        if (Vector3.Distance(this.transform.position, nodes[current].position) < 3f)
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
