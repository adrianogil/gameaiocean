using UnityEngine;
using System.Collections;

public class RandomBezierSpline : MonoBehaviour {

	public BezierSpline randomSpline;

	public float radiusSize = 10f;

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

	}
}
