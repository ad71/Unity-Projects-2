using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform car;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = car.rotation;
        rotation.z = 0;
        this.transform.rotation = rotation;
        this.transform.position = car.position - car.transform.forward * 1.5f + car.transform.up * 0.75f;
	}
}
