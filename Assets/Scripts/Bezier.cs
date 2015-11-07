using UnityEngine;

public static class Bezier {

	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3,  float t)
	{
		//return Vector3.Lerp (Vector3.Lerp (p0, p1, t), Vector3.Lerp (p1, p2, t), t);

		t = Mathf.Clamp01 (t);

		return (1 - t) * (1 - t) * (1 - t) * p0 +
			3 * (1 - t) * (1 - t) * t * p1 + 
			3 * (1 - t) * t * t * p2 + 
			t * t * t * p3;
	}
}
