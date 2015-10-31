using UnityEngine;
using System.Collections;

// Prefab Generation a la Minecraft
public class PrefabGeneration : MonoBehaviour {


	public int floorSizeX = 10;
	public int floorSizeY = 10;

	float prefabSizeX;
	float prefabSizeY;
	float prefabSizeZ;

	public Transform prefab;

	// Use this for initialization
	void Start () {
		Renderer prefabRenderer = prefab.GetComponent<Renderer> ();

		// Get prefab size
		prefabSizeX = prefabRenderer.bounds.size.x;
		prefabSizeY = prefabRenderer.bounds.size.y;
		prefabSizeZ = prefabRenderer.bounds.size.z;

		FloorGeneration ();
	}

	void FloorGeneration()
	{
		Vector3 cubePosition = Vector3.zero;

		for (int y = 0; y < floorSizeY; y++) {

			cubePosition.z += prefabSizeZ;
			cubePosition.x = 0f;

			for (int x = 0; x < floorSizeX; x++) {

				cubePosition.x += prefabSizeX;

				Instantiate (prefab, cubePosition, Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
