using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput;

public class CarAI : MonoBehaviour {

    public Transform path;
    public float currentSpeed = 0f;
    public float maxSpeed = 10f;
    public WheelCollider wheelfl;

    private List<Transform> nodes;
    private int current = 0;
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
        float steer = relative.x / relative.magnitude;
        if (Random.Range(0, 1) <= steer)
            InputSimulator.SimulateKeyPress(VirtualKeyCode.RIGHT);
        else if (Random.Range(-1, 0) >= steer)
            InputSimulator.SimulateKeyPress(VirtualKeyCode.LEFT);
        Debug.DrawLine(this.transform.position, nodes[current].position);
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelfl.radius * wheelfl.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed)
        {
            InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
        }
        else
        {
            InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
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
