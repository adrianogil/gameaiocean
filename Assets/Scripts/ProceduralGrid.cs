using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {

	public int sizeX, sizeY;

	private Vector3[] vertices;
	private int totalVertices;

	private Mesh mesh;

	private void Awake()
	{
		StartCoroutine(Generate());
	}

	private IEnumerator Generate()
	{
		WaitForSeconds wait = new WaitForSeconds (1f);

		GetComponent<MeshFilter>().mesh = mesh = new Mesh ();
		mesh.name = "Procedural Grid";

		#region Vertices Creation
		totalVertices = (sizeX + 1) * (sizeY + 1);

		vertices = new Vector3[totalVertices];

		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {

				vertices[i] = new Vector3(x,y);


			}
		}

		mesh.vertices = vertices;
		#endregion

		int[] triangles = new int[6 * sizeX * sizeY];

		for (int ti = 0, vi = 0, y = 0; y < sizeY; y++, vi++) {
			for (int x = 0; x < sizeX; x++, ti += 6, vi++) {
				triangles [ti] = vi;
				triangles [ti+3] = triangles [ti+2] = vi+1;
				triangles [ti+4] = triangles [ti+1] = vi+sizeX + 1;
				triangles [ti+5] = vi+sizeX + 2;
					
				mesh.triangles = triangles;
				yield return wait;
			}
		}

		mesh.RecalculateNormals ();

		Vector2[] uv = new Vector2[vertices.Length];
		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {
				uv[i] = new Vector2((float)x / sizeX,  (float)y / sizeY);
			}
		}

		mesh.uv = uv;

		Vector4[] tangents = new Vector4[vertices.Length];
		Vector4 tangent = new Vector4 (1f, 0f, 0f, -1f);
		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {
				tangents[i] = tangent;
			}
		}
		mesh.tangents = tangents;

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		for (int i = 0; i < totalVertices; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}

}
