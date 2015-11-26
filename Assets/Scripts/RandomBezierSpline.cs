using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RandomBezierSpline : MonoBehaviour {

	public BezierSpline randomSpline;

	public float radiusSize = 10f;

	public float roadSize = 20f;

	private MeshBuilder meshBuilder = new MeshBuilder ();

	// Use this for initialization
	void Start () {

		randomSpline.Reset();

		int totalCurves = Random.Range(30, 50);

		for (int i = 1; i < totalCurves; i++)
		{
			randomSpline.AddCurve();
		}

		Vector3 point = Vector3.zero;
		float radiusX = 10f, radiusY = 20f;
		float radiusAmount = 0f;

		for (int i = 0; i < randomSpline.CurveCount; i++)
		{
			radiusAmount = ((float)i / randomSpline.CurveCount) * (2*Mathf.PI);
			point = new Vector3(radiusX * Mathf.Cos (radiusAmount), 0f, radiusY * Mathf.Sin (radiusAmount));//+ 
				//new Vector3(Random.Range (-0.5f, 0.5f), 0f, Random.Range (-0.5f, 0.5f));

			point = radiusSize * point;

			randomSpline.SetControlPointMode(i*3, BezierControlPointMode.Free);
			randomSpline.SetControlPoint(i*3, point);

			if (i*3-1 >= 0)
				randomSpline.SetControlPoint(i*3-1, point);

			if (i*3+1 < randomSpline.ControlPointCount)
				randomSpline.SetControlPoint(i*3+1, point);
		}

		randomSpline.SetControlPoint(randomSpline.ControlPointCount-1, point);

		Vector3 lastPoint = randomSpline.GetControlPoint(0);

		for (int i = 1; i < randomSpline.CurveCount; i++) {
			point = randomSpline.GetControlPoint(i*3);

			Vector3 crossVector = Vector3.Cross(point - lastPoint, Vector3.up);

			Vector3 p1 = point + 0.5f * roadSize * crossVector.normalized;
			Vector3 p2 = point - 0.5f * roadSize * crossVector.normalized;

			meshBuilder.Vertices.Add(p1);
			meshBuilder.Vertices.Add(point);
			meshBuilder.Vertices.Add(p2);

			lastPoint = point;
		}

		int baseIndex = 0;
		int sizeX = 2;
		int sizeY = randomSpline.CurveCount - 2;

		int vi = baseIndex;

		for (int y = 0; y < sizeY; y++, vi++) {
			for (int x = 0; x < sizeX; x++, vi++) {
				meshBuilder.AddTriangle (vi, vi + sizeX + 1, vi + 1);
				meshBuilder.AddTriangle (vi + 1, vi + sizeX + 1, vi + sizeX + 2);

				if (y == sizeY - 1) {
					meshBuilder.AddTriangle (vi + sizeX + 1, baseIndex + x, vi + sizeX + 2);
					meshBuilder.AddTriangle (baseIndex + x + 1, vi + sizeX + 2, baseIndex + x);
				}
			}
		}

		GetComponent<MeshFilter> ().mesh = meshBuilder.CreateMesh ();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.black;

		for (int i = 0; i < meshBuilder.Vertices.Count; i++)
			Gizmos.DrawSphere(transform.TransformPoint(meshBuilder.Vertices[i]), 0.5f);
	}
}
