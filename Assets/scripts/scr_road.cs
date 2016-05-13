using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_road : MonoBehaviour {

    // Road variables
    private static float areaHeight = 60f;
    private static int numLanes = 6;
    private static float laneWidth = 3f;
    private static float sideWidth = 1f;
    private static float areaWidth = sideWidth * 2 + laneWidth * numLanes;

    // Spawning variables
    private Vector2 sideWeight = new Vector2(0f, 0f);
    private float difficulty = 0f;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
