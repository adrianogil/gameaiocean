using UnityEngine;
using System.Collections;

public class PrefabGeneration : MonoBehaviour {

	public Transform prefab;

	// Use this for initialization
	void Start () {

		Instantiate (prefab, Vector3.zero, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
