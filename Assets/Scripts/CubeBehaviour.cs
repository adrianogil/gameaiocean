using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour {

	public float velocity = 10f;

	float horizontalAxis;
	Rigidbody cubeRigidBody;

	// Use this for initialization
	void Start () {
		cubeRigidBody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		horizontalAxis = Input.GetAxis ("Horizontal");

		if (horizontalAxis > 0) {
			transform.Rotate (0f, 0f, 1f);
			cubeRigidBody.velocity += velocity * Vector3.right * Time.deltaTime;
		} else if (horizontalAxis < 0) {
			transform.Rotate (0f, 0f, -1f);
			cubeRigidBody.velocity += velocity * Vector3.left * Time.deltaTime;
		}

	}
}
