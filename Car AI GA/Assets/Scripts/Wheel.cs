using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    public WheelCollider target;
    private Vector3 position = new Vector3();
    private Quaternion rotation = new Quaternion();
	void Update () {
        target.GetWorldPose(out position, out rotation);
        this.transform.position = position;
        this.transform.rotation = rotation;
	}
}
