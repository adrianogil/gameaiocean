using UnityEngine;
using System.Collections;

public enum SplineWalkerMode
{
	Once,
	Loop,
	PingPong
}

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float duration;

	private float progress;

	private bool goingForward = true;

	public SplineWalkerMode mode;

	// Update is called once per frame
	void Update () {
		if (goingForward) {
			progress += Time.deltaTime / duration;

			if (progress > 1f) {

				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				} else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				} else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		} else {
			progress -= Time.deltaTime / duration;

			if (progress < 0f)
			{
				progress = -progress;
				goingForward = true;
			}

		}

		transform.localPosition = spline.GetPoint (progress);
	}
}
