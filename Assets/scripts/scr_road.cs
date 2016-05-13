using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_road : MonoBehaviour {

    // Road variables
    private float areaWidth = 16f;
    private int numLanes = 6;
    private float[] spawnPositions = new float[6] { 2.91f, 1.75f, 0.63f, -0.63f, -1.75f, -2.91f };
    private Color[] carColors = new Color[5] { Color.red, Color.blue, Color.green, Color.black, Color.white };

    // Spawning variables
    private Vector2 sideWeight = new Vector2(0f, 0f);
    private float difficulty;
    private float acceleration = .99f;
    private float carSpeed = 2f;
    private float spawnTimer = 1.5f;

    // Prefabs
    public scr_car carObj;

	// Use this for initialization
	void Start () {
        difficulty = spawnTimer;
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
                tempCar.transform.position = new Vector3(areaWidth * .75f, spawnPositions[offset + position], 0f);
                tempCar.GetComponent<Rigidbody2D>().velocity = new Vector3(-carSpeed, 0f, 0f);
                tempCar.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
            }
            else
            {
                tempCar.transform.position = new Vector3(-areaWidth * .75f, spawnPositions[offset + position], 0f);
                tempCar.GetComponent<Rigidbody2D>().velocity = new Vector3(carSpeed, 0f, 0f);
                tempCar.transform.rotation = Quaternion.Euler(new Vector3(-90f, -90f, -90f));
            }
            Color randColor = carColors[Random.Range(0,carColors.Length)];
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
