using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_camera : MonoBehaviour {

    Vector3 basePos;
    List<Vector3> shakeList = new List<Vector3>();
    float shakeDelay = 0.02f;
    float shakeTime = 0.0f;
    float shakeStrength = 0.3f;
    int numShakes = 20;

    // Use this for initialization
    void Start () {

        basePos = transform.position;

	}

    // Update is called once per frame
    void Update() {

        shakeTime -= Time.deltaTime;

        if (shakeTime <= 0) {
            shakeTime = 0;
            if (shakeList.Count > 0)
            {
                transform.position = shakeList[0];
                shakeList.RemoveAt(0);
                shakeTime = shakeDelay;
            }
            else
            {
                transform.position = basePos;
            }
        }

	}

    public void Shake()
    {
        for (var i = 0; i < numShakes; i++)
        {
            float decay = Mathf.Pow(1 - ((float) i / numShakes), 3);
            float x = basePos.x + Mathf.Sign(Random.Range(-1f,1f)) * shakeStrength * decay;
            float y = basePos.y + Mathf.Sign(Random.Range(-1f, 1f)) * shakeStrength * decay;
            float z = basePos.z + Mathf.Sign(Random.Range(-1f, 1f)) * shakeStrength * decay;
            shakeList.Add(new Vector3(x, y, z));
        }
    }
}
