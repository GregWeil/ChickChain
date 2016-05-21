using UnityEngine;
using System.Collections;

public class scr_aspect : MonoBehaviour {

    float aspect = (16f / 9f);
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        var scale = new Vector2((Screen.height * aspect) / Screen.width,
                                (Screen.width / aspect) / Screen.height);
        var view = Vector2.Min(scale, Vector2.one);
        cam.rect = new Rect((Vector2.one - view) / 2f, view);
    }
}
