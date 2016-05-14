using UnityEngine;
using System.Collections.Generic;

public class ChickenPathRecord : MonoBehaviour {

    List<Vector2> positions = new List<Vector2>();

    float interval = 0.1f;
    int history = 1;

	// Use this for initialization
	void Start () {
        positions.Add(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        var positionLast = positions[positions.Count - 1];
        if (Vector2.Distance(positionLast, transform.position) > interval) {
            positions.Add(transform.position);
        }
        while (positions.Count > history) {
            positions.RemoveAt(0);
        }
	}

    public Vector2 pathPosition (float distance) {
        int index = Mathf.RoundToInt(distance / interval);
        if (index >= positions.Count) {
            history = (index + 1);
            index = (positions.Count - 1);
        }
        return positions[positions.Count - index - 1];
    }
}
