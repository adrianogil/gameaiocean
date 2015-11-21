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



		transform.position = path.GetPoint (progress);
	}
}
