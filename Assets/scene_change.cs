using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class scene_change : MonoBehaviour {

    public float timer = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            SceneManager.LoadScene("scene_road");
        }
	}
}
