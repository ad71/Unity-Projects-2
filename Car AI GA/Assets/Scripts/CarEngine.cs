using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 40f;

    [Header("Colliders")]
    public WheelCollider wheelfr;
    public WheelCollider wheelfl;
    public WheelCollider wheelrr;
    public WheelCollider wheelrl;

    private List<Transform> nodes;
    private int current = 0;

    private void Start()
    {
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; ++i)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        Steer();
    }

    private void Steer()
    {
        Vector3 relative = this.transform.InverseTransformPoint(nodes[0].position);
        // float steer = (relative.x / relative.magnitude) * maxSteerAngle;
        // wheelfl.steerAngle = steer;
        // wheelfr.steerAngle = steer;
        // To do: Mutation might let rear wheels turn
    }
}
