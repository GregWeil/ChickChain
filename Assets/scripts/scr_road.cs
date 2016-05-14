using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_road : MonoBehaviour {

    // Road variables
    private float areaWidth = 16f;
    private int numLanes = 6;
    private float[] spawnPositions = new float[6] { 2.84f, 1.75f, 0.64f, -0.66f, -1.76f, -2.86f };

    // Spawning variables
    private Vector2 sideWeight = new Vector2(0f, 0f);
    private float difficulty;
    private float acceleration = .99f;
    private float carSpeed = 2f;
    private float spawnTimer = 1.5f;

    // Crack variables
    private List<scr_crack> crackList = new List<scr_crack>();
    private Vector2 crackLength = new Vector2(1f, 2f);
    private Vector2 crackWidth = new Vector2(0.3f, 0.5f);
    private float crackRadius = 3f;
    private int numCracks;

    // Prefabs
    public scr_car carObj;
    public scr_crack crackObj;

	// Use this for initialization
	void Start () {
        difficulty = spawnTimer;
        numCracks = Random.Range(15, 20);

        // Spawn cracks
        for (var i = 0; i < numCracks; i++)
        {
            scr_crack current = Instantiate(crackObj);

            current.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, Random.Range(0f, 360f)));
            current.transform.localScale = new Vector3(Random.Range(crackLength.x, crackLength.y), Random.Range(crackWidth.x, crackWidth.y), 1f);

            int limit = 500;
            while (limit > 0)
            {
                int matches = 0;
                current.transform.position = new Vector3(Random.Range(-areaWidth/2, areaWidth/2), Random.Range(-3f, 3f), 0.01f);
                for (var j = 0; j < crackList.Count; j++)
                {
                    scr_crack temp = crackList[j];
                    float distance = Vector3.Distance(current.transform.position, temp.transform.position);
                    if (distance < crackRadius) { matches++; }
                }
                limit--;
                if (matches < 2) { limit = 0; }
            }
            crackList.Add(current);
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Spawn cars
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            difficulty *= acceleration;
            spawnTimer = difficulty;

            scr_car tempCar = Instantiate(carObj);
            int offset = chooseSide();
            int position = Random.Range(0, numLanes / 2);

            if (offset >= numLanes / 2)
            {
                tempCar.transform.position = new Vector3(areaWidth * .75f, spawnPositions[offset + position], 0.0f);
                tempCar.GetComponent<Rigidbody2D>().velocity = new Vector3(-carSpeed, 0f, 0f);
                tempCar.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            }
            else
            {
                tempCar.transform.position = new Vector3(-areaWidth * .75f, spawnPositions[offset + position], 0.0f);
                tempCar.GetComponent<Rigidbody2D>().velocity = new Vector3(carSpeed, 0f, 0f);
                tempCar.transform.rotation = Quaternion.Euler(new Vector3(-90f, -90f, -90f));
            }
            Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            tempCar.carColor.GetComponent<Renderer>().material.SetColor("_Color", randColor);
            tempCar.carColor.GetComponent<Renderer>().material.SetColor("_SpecColor", randColor);
            tempCar.carColor.GetComponent<Renderer>().material.SetColor("_EmissionColor", randColor);
        }

    }

    // Choose to spawn a car going left, or a car going right
    private int chooseSide()
    {
        float leftWeight = Random.Range(0f, 5f) + sideWeight.x;
        float rightWeight = Random.Range(0f, 5f) + sideWeight.y;
        if (leftWeight > rightWeight)
        {
            sideWeight.x--;
            sideWeight.y++;
            return numLanes / 2;
        }
        else
        {
            sideWeight.x++;
            sideWeight.y--;
            return 0;
        }
    }
}
