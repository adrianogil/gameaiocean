using UnityEngine;
using System.Collections;
using GameAnalyticsSDK;

public class GameAnalyticsManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("loaded_maze", PlayerPrefs.GetInt ("loaded_maze", 0) + 1);

		GameAnalytics.NewDesignEvent ("maze_scene:loading", PlayerPrefs.GetInt ("loaded_maze", 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
