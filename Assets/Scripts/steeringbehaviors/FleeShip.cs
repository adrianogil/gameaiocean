﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FleeShip : MonoBehaviour {
	
	public Transform target;
	
	public float velocity;
	
	private Rigidbody2D shipRigidBody;
	
	// Use this for initialization
	void Start () {
		shipRigidBody = GetComponent<Rigidbody2D> ();
		
		shipRigidBody.velocity = transform.up * velocity;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diff = transform.position - target.position;
		
		if (diff.magnitude > 10f) {
			shipRigidBody.velocity = Vector3.zero;
			return;
		}
		
		Vector3 desiredVelocity = diff.normalized * velocity;
		
		
		
		shipRigidBody.velocity = desiredVelocity;
		
		transform.Rotate (0f, 0f, 
		                  - Angle.Short(Vector3.Angle (desiredVelocity, transform.up)) * Time.deltaTime
		                  );
	}
}
