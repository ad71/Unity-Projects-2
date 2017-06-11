using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 40f;
    public float currentSpeed = 0f;
    public float topSpeed = 100f;
    public float maxMotorTorque = 100f;
    public float maxBrakingTorque = 200f;
    public Vector3 centerofMass;
    public bool isBraking = false;
    public Texture2D normal;
    public Texture2D braking;
    public Renderer carTextureRenderer;

    [Header("Colliders")]
    public WheelCollider wheelfr;
    public WheelCollider wheelfl;
    public WheelCollider wheelrr;
    public WheelCollider wheelrl;

    private List<Transform> nodes;
    private int current = 0;

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerofMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; ++i)
            if (pathTransforms[i] != path.transform)
                nodes.Add(pathTransforms[i]);
    }

    private void FixedUpdate()
    {
        Steer();
        Drive();
        Next();
        Brake();
    }

    private void Steer()
    {
        Vector3 relative = this.transform.InverseTransformPoint(nodes[current].position);
        float steer = (relative.x / relative.magnitude) * maxSteerAngle;
        wheelfl.steerAngle = steer;
        wheelfr.steerAngle = steer;
        // To do: Mutation might let rear wheels turn
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelfl.radius * wheelfl.rpm * 60 / 1000;
        if (currentSpeed < topSpeed)
        {
            wheelfl.motorTorque = maxMotorTorque;
            wheelfr.motorTorque = maxMotorTorque;
            // to do: Mutation might make it 4x4
        }
        else
        {
            wheelfr.motorTorque = 0;
            wheelfl.motorTorque = 0;
        }
    }

    private void Next()
    {
        if (Vector3.Distance(this.transform.position, nodes[current].position) < 3f)
        {
            if (current == nodes.Count - 1) current = 0;
            else current++;
        }
    }

    private void Brake()
    {
        // to do: Mutation might apply brakes on all four wheels
        if (isBraking)
        {
            carTextureRenderer.material.mainTexture = braking;
            wheelrl.brakeTorque = maxBrakingTorque;
            wheelrr.brakeTorque = maxBrakingTorque;
        }
        else
        {
            carTextureRenderer.material.mainTexture = normal;
            wheelrr.brakeTorque = 0;
            wheelrl.brakeTorque = 0;
        }
    }
}
