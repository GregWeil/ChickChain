using UnityEngine;
using System.Collections;

public class scr_car : MonoBehaviour {

    public GameObject carColor;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        // Destroy if out of bounds
	    if (Mathf.Abs(transform.position.x) > 16f * .8f)
        {
            Destroy(gameObject);
        }
	}
}
