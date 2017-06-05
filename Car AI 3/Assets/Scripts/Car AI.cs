using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 35f;

    [Header("Wheel Colliders")]
    public WheelCollider wheelfl;
    public WheelCollider wheelfr;
    public WheelCollider wheelrl;
    public WheelCollider wheelrr;

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
	}

    private void Steer()
    {
        Vector3 relative = this.transform.InverseTransformPoint(nodes[0].position);
        float steer = relative.x / relative.magnitude * maxSteerAngle;
        wheelfl.steerAngle = steer;
        wheelfr.steerAngle = steer;
    }
}
