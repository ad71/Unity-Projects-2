using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 40f;
    public float currentSpeed = 0f;
    public float turningSpeed = 5f;
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

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 sensorPosition = new Vector3(0, 0.2f, 0.5f);
    public float sideSensorOffset = 0.2f;
    public float sensorSkewAngle = 30f;

    private List<Transform> nodes;
    private int current = 0;
    private bool avoiding = false;
    private float targetSteerAngle = 0f;

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
        Sense();
        Steer();
        Drive();
        Next();
        Brake();
        Check();
        Angle();
        // To do: find conditions for braking
    }

    private void Steer()
    {
        if (avoiding) return;
        Vector3 relative = this.transform.InverseTransformPoint(nodes[current].position);
        float steer = (relative.x / relative.magnitude) * maxSteerAngle;
        // if(lerpToSteerAngle?)
        targetSteerAngle = steer;

        // else
        // wheelfl.steerAngle = steer;
        // wheelfr.steerAngle = steer;
        // To do: Mutation might let rear wheels turn
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelfl.radius * wheelfl.rpm * 60 / 1000;
        if (currentSpeed < topSpeed && !isBraking)
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

    private void Check()
    {
        // To check braking conditions
        if (currentSpeed > 0.2 * topSpeed && wheelfl.steerAngle > 0.2 * maxSteerAngle) isBraking = true;
        else isBraking = false;
    }

    private void Sense()
    {
        RaycastHit hit;
        Vector3 origin = this.transform.position;
        origin += this.transform.forward * sensorPosition.z;
        origin += this.transform.up * sensorPosition.y;
        float avoidMultiplier = 0f;
        avoiding = false;

        // Front right sensor
        origin += transform.right * sideSensorOffset;
        if (Physics.Raycast(origin, this.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(origin, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }

        // Front right skew sensor
        // Quaternion.AngleAxis turns right by sensorSkewAngle about the transform.up axis relative to the car
        else if (Physics.Raycast(origin, Quaternion.AngleAxis(sensorSkewAngle, this.transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(origin, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        //Front left sensor
        origin -= 2 * transform.right * sideSensorOffset;
        if (Physics.Raycast(origin, this.transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(origin, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }

        // Front right sensor
        // Quaternion.AngleAxis turns left by sensorSkewAngle about the transform.up axis relative to the car
        else if (Physics.Raycast(origin, Quaternion.AngleAxis(-sensorSkewAngle, this.transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Terrain"))
            {
                Debug.DrawLine(origin, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(origin, this.transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(origin, hit.point);
                    avoiding = true;
                    if (hit.normal.x < 0) avoidMultiplier = -1;
                    else avoidMultiplier = 1;
                }
            }
        }

        if (avoiding)
        {
            // if(lerpToSteerangle?)
            targetSteerAngle = maxSteerAngle * avoidMultiplier;

            // else
            // wheelfl.steerAngle = maxSteerAngle * avoidMultiplier;
            // wheelfr.steerAngle = maxSteerAngle * avoidMultiplier;
            // To do: Mutation might allow turning of rear wheels
        }
    }

    private void Angle()
    {
        wheelfl.steerAngle = Mathf.Lerp(wheelfl.steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);
        wheelfr.steerAngle = Mathf.Lerp(wheelfl.steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);

        // if rear wheels can turn,
        // wheelrr.steerAngle = Mathf.Lerp(wheelrr.steerAngle, -targetSteerAngle, Time.deltaTime * turningSpeed);
        // wheelrl.steerAngle = Mathf.Lerp(wheelrl.steerAngle, -targetSteerAngle, Time.deltaTime * turningSpeed);
    }
}