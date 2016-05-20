using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_camera : MonoBehaviour {

    Vector3 basePos;
    Vector3 baseRot;
    List<Vector3> shakeList = new List<Vector3>();
    float shakeDelay = 0.02f;
    float shakeTime = 0.0f;
    float shakeStrength = 0.3f;
    int numShakes = 20;

    bool doneMoving = false;

    public Material fadeMat;

    // Use this for initialization
    void Start () {

        basePos = new Vector3(0f, -6.5f, 8f);
        baseRot = new Vector3(315f, 180f, 0f);
        fadeMat.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
    }

    // Update is called once per frame
    void Update() {

        if (scr_road.endTime < 1.5f)
        {
            fadeMat.SetColor("_Color", new Color(0f, 0f, 0f, 1f - scr_road.endTime / 1.5f));
        }

        if (scr_road.gameStart)
        {
            if (!doneMoving) {
                transform.position = Vector3.Lerp(transform.position, basePos, 0.05f);
                transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, baseRot, 0.05f));
                if (Vector3.Distance(transform.position, basePos) < 0.01f) { doneMoving = true; }
            }

            if (doneMoving)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
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
