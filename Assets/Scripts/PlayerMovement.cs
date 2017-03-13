using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	Rigidbody rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		rigidBody.AddForce(Input.GetAxis("Horizontal"), 0, 0);
		//transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0);
	}
}
