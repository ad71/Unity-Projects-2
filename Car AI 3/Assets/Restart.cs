using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

	public GameObject user;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			user.transform.position = new Vector3 (29.919f, -17.175f, -106.389f);
			user.transform.rotation = new Quaternion (0f, 90f, 0f);
		}
	}
}