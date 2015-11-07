using UnityEngine;
using System.Collections;

public class PCGUtils {

	public static GameObject CreatePrefabMatrix(Transform prefab,
	                                            Vector3 prefabSize,
	                                            Vector3 initialPosition,
	                                            int sizeX, int sizeY = 1, int sizeZ = 1)
	{
		GameObject gameObject = new GameObject ();

		Vector3 cubePosition = initialPosition;
		Transform cube;
		
		for (int y = 0; y < sizeY; y++) {
			
			cubePosition.z += prefabSize.z;
			cubePosition.x = 0f;
			
			for (int x = 0; x < sizeX; x++) {
				
				cubePosition.x += prefabSize.x;
				
				cube = Object.Instantiate (prefab, cubePosition, Quaternion.identity) as Transform;
				
				cube.parent = gameObject.transform;
			}
		}

		return gameObject;
	}
}
