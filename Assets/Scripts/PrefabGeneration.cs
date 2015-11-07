using UnityEngine;
using System.Collections;

// Prefab Generation a la Minecraft
public class PrefabGeneration : MonoBehaviour {


	public int floorSizeX = 10;
	public int floorSizeY = 10;

	float prefabSizeX;
	float prefabSizeY;
	float prefabSizeZ;

	Vector3 prefabSize;

	public Transform prefab;

	// Use this for initialization
	void Start () {
		Renderer prefabRenderer = prefab.GetComponent<Renderer> ();

		// Get prefab size
		prefabSizeX = prefabRenderer.bounds.size.x;
		prefabSizeY = prefabRenderer.bounds.size.y;
		prefabSizeZ = prefabRenderer.bounds.size.z;

		prefabSize.x = prefabRenderer.bounds.size.x;
		prefabSize.y = prefabRenderer.bounds.size.y;
		prefabSize.z = prefabRenderer.bounds.size.z;

		FloorGeneration ();

		TreeGeneration ();
	}

	void FloorGeneration()
	{

		Vector3 initialPosition = Vector3.zero;

		PCGUtils.CreatePrefabMatrix (prefab,
		                            prefabSize,
		                            initialPosition,
		                            Random.Range (floorSizeX, 2*floorSizeX),
		                            Random.Range (floorSizeY, 2*floorSizeY));
	}

	void TreeGeneration()
	{
		int treePosX = 0;
		int treePosY = 0;

		int trunkSize = 5;

		int treeSizeX = 5;
		int treeSizeY = 5;
		int treeSizeZ = 5;

		Vector3 treePosition = new Vector3((treePosX+1) * prefabSizeX, 
		                                   prefabSizeY,
		                                   (treePosY+1) * prefabSizeZ);

		Transform cube;
		Vector3 cubePosition = treePosition;

		GameObject tree = new GameObject ("Tree");

		for (int t = 0; t < trunkSize; t++) {
			cube = Instantiate (prefab, cubePosition, Quaternion.identity) as Transform;
			cube.transform.parent = tree.transform;

			cubePosition.y += prefabSizeY;
		}

		cubePosition.x -= (treeSizeX/2) * prefabSizeX;
		cubePosition.z -= (treeSizeZ/2) * prefabSizeZ;

		treePosition = cubePosition;


		for (int z = 0; z < treeSizeZ; z++) {
			cubePosition.y = treePosition.y;
			for (int y = 0; y < treeSizeY; y++) {
				cubePosition.x = treePosition.x;
				for (int x = 0; x < treeSizeX; x++) {
					cube = Instantiate (prefab, cubePosition, Quaternion.identity) as Transform;
					cube.transform.parent = tree.transform;
					cubePosition.x += prefabSizeX;
				}
				cubePosition.y += prefabSizeY;
			}
			cubePosition.z += prefabSizeZ;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
