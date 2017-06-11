using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public float currentSpeed = 0f;
    public Transform path;
    public Texture2D normal;
    public Texture2D braking;
    public Renderer carTextureRenderer;
    public bool isBraking = false;
    public bool verbose = false;
    private Vector3 centerofMass;
    private float maxSteerAngle = 0f;
    private float topSpeed = 0f;
    private float maxMotorTorque = 0f;
    private float maxBrakingTorque = 0f;   
    private float mass = 0f;
    private float sensorLength = 0f;
    private float sensorSkewAngle = 0f;
    private float switchToNextwaypointDistance = 0f;
    private float brakeTopSpeedMultiplier;
    private float brakeSteerMultiplier;
    private float avoidMultiplierMultiplier;
    private float turningSpeed = 0f;
    private bool fourWheelDrive;
    private bool fourWheelBrake;
    private bool fourWheelTurn;
    private bool doesnotGiveAFuck;
    private bool doesItLerp;

    [Header("Colliders")]
    public WheelCollider wheelfr;
    public WheelCollider wheelfl;
    public WheelCollider wheelrr;
    public WheelCollider wheelrl;

    [Header("Sensors")]
    public Vector3 sensorPosition = new Vector3(0, 0.2f, 0.5f);
    public float sideSensorOffset = 0.2f;

    private List<Transform> nodes;
    private int current = 0;
    private bool avoiding = false;
    private float targetSteerAngle = 0f;
    private DNA dna;

    private void Init(DNA dna)
    {
        maxSteerAngle = Mathf.Lerp(20f, 50f, dna.genes[0]);
        topSpeed = Mathf.Lerp(50f, 150f, dna.genes[1]);
        maxMotorTorque = Mathf.Lerp(50f, 200f, dna.genes[2]);
        maxBrakingTorque = Mathf.Lerp(50f, 400f, dna.genes[3]);
        centerofMass = new Vector3(0, Mathf.Lerp(-0.3f, 0.3f, dna.genes[4]), 0);
        mass = Mathf.Lerp(500f, 1500f, dna.genes[5]);
        // Sensors do not matter now, as we are trying to find the global minima of time taken to traverse the track
        sensorLength = Mathf.Lerp(1f, 5f, dna.genes[6]);
        sensorSkewAngle = Mathf.Lerp(20f, 70f, dna.genes[7]);
        if (Random.Range(0f, 1f) < dna.genes[8]) fourWheelDrive = true;
        if (Random.Range(0f, 1f) < dna.genes[9]) fourWheelBrake = true;
        if (10f * Random.Range(0f, 1f) < dna.genes[10]) fourWheelTurn = true;
        switchToNextwaypointDistance = Mathf.Lerp(1f, 7f, dna.genes[11]);
        if (Random.Range(0f, 1f) < dna.genes[12]) doesnotGiveAFuck = true;
        brakeTopSpeedMultiplier = Mathf.Lerp(0.2f, 0.75f, dna.genes[13]);
        brakeSteerMultiplier = Mathf.Lerp(0.2f, 0.75f, dna.genes[14]);
        avoidMultiplierMultiplier = Mathf.Lerp(0.5f, 1f, dna.genes[15]);
        if (Random.Range(0f, 1f) < dna.genes[16]) doesItLerp = true;
        turningSpeed = Mathf.Lerp(2f, 10f, dna.genes[17]);

        if (verbose)
        {
            Debug.Log("Max Steer Angle: " + maxSteerAngle);
            Debug.Log("Top Speed: " + topSpeed);
            Debug.Log("Max Motor Torque: " + maxMotorTorque);
            Debug.Log("Max Braking Torque: " + maxBrakingTorque);
            Debug.Log("Center of mass: (" + centerofMass.x + ", " + centerofMass.y + ", " + centerofMass.z + ")");
            Debug.Log("Mass: " + mass + "kg");
            Debug.Log("Sensor length: " + sensorLength);
            Debug.Log("Sensor skew angle: " + sensorSkewAngle);
            Debug.Log("4 Wheel drive: " + fourWheelDrive);
            Debug.Log("4 Wheel brake: " + fourWheelBrake);
            Debug.Log("4 Wheel turn: " + fourWheelTurn);
            Debug.Log("Switch to next waypoint distance: " + switchToNextwaypointDistance);
            Debug.Log("Rowdy: " + doesnotGiveAFuck);
            Debug.Log("Brake top speed multiplier: " + brakeTopSpeedMultiplier);
            Debug.Log("Brake steer multiplier: " + brakeSteerMultiplier);
            Debug.Log("Avoid multiplier magnitude: " + avoidMultiplierMultiplier);
            Debug.Log("Smooth turning: " + doesItLerp);
            Debug.Log("Turning speed: " + turningSpeed);
        }
    }

    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; ++i)
            if (pathTransforms[i] != path.transform)
                nodes.Add(pathTransforms[i]);
        dna = new DNA();
        Init(dna);
        GetComponent<Rigidbody>().centerOfMass = centerofMass;
        GetComponent<Rigidbody>().mass = mass;
    }

    private void FixedUpdate()
    {
        if (doesnotGiveAFuck)
        {
            Sense();
        }
        Steer();
        Drive();
        Next();
        Brake();
        Check();
        if (doesItLerp)
        {
            Angle();
        }
    }

    private void Steer()
    {
        if (avoiding) return;
        Vector3 relative = this.transform.InverseTransformPoint(nodes[current].position);
        float steer = (relative.x / relative.magnitude) * maxSteerAngle;
        // if(lerpToSteerAngle?)
        if (doesItLerp)
        {
            targetSteerAngle = steer;
        }
        else
        {
            wheelfl.steerAngle = steer;
            wheelfr.steerAngle = steer;
            if (fourWheelTurn)
            {
                wheelrl.steerAngle = -steer;
                wheelrr.steerAngle = -steer;
            }
        }
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelfl.radius * wheelfl.rpm * 60 / 1000;
        if (currentSpeed < topSpeed && !isBraking)
        {
            wheelrl.motorTorque = maxMotorTorque;
            wheelrr.motorTorque = maxMotorTorque;
            // to do: Mutation might make it 4x4
            if (fourWheelDrive)
            {
                wheelfr.motorTorque = maxMotorTorque;
                wheelfl.motorTorque = maxMotorTorque;
            }
        }
        else
        {
            wheelrr.motorTorque = 0;
            wheelrl.motorTorque = 0;
            if (fourWheelDrive)
            {
                wheelfr.motorTorque = 0;
                wheelfl.motorTorque = 0;
            }
        }
    }

    private void Next()
    {
        if (Vector3.Distance(this.transform.position, nodes[current].position) < switchToNextwaypointDistance)
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
            if (fourWheelBrake)
            {
                wheelfl.brakeTorque = maxBrakingTorque;
                wheelfr.brakeTorque = maxBrakingTorque;
            }
        }
        else
        {
            carTextureRenderer.material.mainTexture = normal;
            wheelrr.brakeTorque = 0;
            wheelrl.brakeTorque = 0;
            if (fourWheelBrake)
            {
                wheelfl.brakeTorque = maxBrakingTorque;
                wheelfr.brakeTorque = maxBrakingTorque;
            }
        }
    }

    private void Check()
    {
        // To check braking conditions
        if (currentSpeed > brakeTopSpeedMultiplier * topSpeed && wheelfl.steerAngle > brakeSteerMultiplier * maxSteerAngle) isBraking = true;
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
                avoidMultiplier -= avoidMultiplierMultiplier;
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
                avoidMultiplier -= 0.5f * avoidMultiplierMultiplier;
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
                avoidMultiplier += avoidMultiplierMultiplier;
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
                avoidMultiplier += 0.5f * avoidMultiplierMultiplier;
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
                    if (hit.normal.x < 0) avoidMultiplier = -avoidMultiplierMultiplier;
                    else avoidMultiplier = avoidMultiplierMultiplier;
                }
            }
        }

        if (avoiding)
        {
            // if(lerpToSteerangle?)
            if (doesItLerp)
                targetSteerAngle = maxSteerAngle * avoidMultiplier;

            else
            {
                wheelfl.steerAngle = maxSteerAngle * avoidMultiplier;
                wheelfr.steerAngle = maxSteerAngle * avoidMultiplier;
                if (fourWheelTurn)
                {
                    wheelrr.steerAngle = -maxSteerAngle * avoidMultiplier;
                    wheelrl.steerAngle = -maxSteerAngle * avoidMultiplier;
                }
            }
            // To do: Mutation might allow turning of rear wheels
        }
    }

    private void Angle()
    {
        wheelfl.steerAngle = Mathf.Lerp(wheelfl.steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);
        wheelfr.steerAngle = Mathf.Lerp(wheelfl.steerAngle, targetSteerAngle, Time.deltaTime * turningSpeed);

        // if rear wheels can turn,
        if (fourWheelTurn)
        {
            wheelrr.steerAngle = Mathf.Lerp(wheelrr.steerAngle, -targetSteerAngle, Time.deltaTime * turningSpeed);
            wheelrl.steerAngle = Mathf.Lerp(wheelrl.steerAngle, -targetSteerAngle, Time.deltaTime * turningSpeed);
        }
    }
}