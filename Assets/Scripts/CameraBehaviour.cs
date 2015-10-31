using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		transform.position = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position;
	}
}
