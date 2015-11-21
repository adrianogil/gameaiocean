using UnityEngine;
using System.Collections;

public class FollowPathShip : MonoBehaviour {

	public BezierSpline path;

	public float duration;

	private float progress;

	// Update is called once per frame
	void Update () {
		progress += Time.deltaTime / duration;

		if (progress >= 1f) {
			progress -= 1f;
		}

		transform.Rotate (0f, 0f, 
		                  - Angle.Short(Vector3.Angle (path.GetDirection (progress), transform.up)) * Time.deltaTime
		);

		transform.position = path.GetPoint (progress);
	}
}

static class Angle
{
	public static float Short(float a)
	{
		if (a > 180f) {
			return a - 360f; // [-180f, 0]
		}

		return a; // [0, 180f]
	}
}
