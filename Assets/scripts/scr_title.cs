using UnityEngine;
using System.Collections;

public class scr_title : MonoBehaviour {

    private Vector3 basePos;
    private float rise = 0;
    private float dist = 0.1f;
    private float offset = 0f;

	// Use this for initialization
	void Start () {

        basePos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(transform.position.x, transform.position.y, basePos.z + (offset * dist) + rise);

        if (scr_road.gameStart)
        {
            rise = Mathf.Lerp(rise, 5f, 0.005f);
        }

        else
        {
            offset = Mathf.Sin(Time.time * 2);
        }

	}
}
