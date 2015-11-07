using UnityEngine;
using System.Collections;

public class SplineDecorator : MonoBehaviour {

	public BezierSpline spline;

	public int frequency;

	public Transform[] prefabs;

	void Awake()
	{
		if (frequency <= 0 || prefabs == null || prefabs.Length == 0) {
			return;
		}

		float stepSize = 1f / (frequency * prefabs.Length);

		for (int p = 0, f = 0; f < frequency; f++) {
			for (int i = 0; i < prefabs.Length; i++, p++)
			{
				Transform go = Instantiate(prefabs[i]) as Transform;
				Vector3 position = spline.GetPoint(p * stepSize);
				go.localPosition = position;
				go.parent = transform;
			}
		}

	}

}
