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
    private float spawnTimer = 1.75f;
    int lastLane = -1;

    // Crack variables
    private List<scr_crack> crackList = new List<scr_crack>();
    private float crackRadius = 3f;
    private float crackRatio = 2f;
    private int numCracks;

    // Egg variables
    public float eggSign = 1f;

    // Prefabs
    public scr_car carObj;
    public scr_crack crackObj;
    public EggPickup eggObj;

	// Use this for initialization
	void Start () {
        difficulty = spawnTimer;
        numCracks = Random.Range(15, 20);

        // Spawn cracks
        for (var i = 0; i < numCracks; i++)
        {
            scr_crack current = Instantiate(crackObj);

            current.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, Random.Range(0f, 360f)));

            int limit = 500;
            while (limit > 0)
            {
                int matches = 0;
                current.transform.position = new Vector3(Mathf.Round(Random.Range(-areaWidth/2, areaWidth/2)), Mathf.Round(Random.Range(-3f, 3f)), 0.0001f);
                current.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f * Random.Range(0, 2)));
                current.transform.localScale = new Vector3(current.transform.localScale.x * (-1 * Random.Range(1,2)), current.transform.localScale.y * (-1 * Random.Range(1, 2)), 1f);
                for (var j = 0; j < crackList.Count; j++)
                {
                    scr_crack temp = crackList[j];
                    float distance = crackDistance(current, temp);
                    if (distance < crackRadius) { matches++; }
                }
                limit--;
                if (matches < 1) { limit = 0; }
            }
            crackList.Add(current);
        }

        // Spawn initial egg
        eggSign = Mathf.Sign(Random.Range(-1f, 1f));
        makeEgg();
        
	}
	
	// Update is called once per frame
	void Update () {

        // Spawn eggs
        if (FindObjectsOfType<EggPickup>().Length == 0)
        {
            makeEgg();
        }

        // Spawn cars
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            difficulty *= acceleration;
            difficulty = Mathf.Max(difficulty, 0.5f);
            spawnTimer = difficulty;

            scr_car tempCar = Instantiate(carObj);
            int offset = chooseSide();
            int position = Random.Range(0, numLanes / 2);

            // Don't let cars overlap when spawning gets fast
            if (difficulty < .75f) {
                while (lastLane == offset + position)
                {
                    position = Random.Range(0, numLanes / 2);
                }
            }

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
            tempCar.carColor.GetComponent<Renderer>().material.SetColor("_EmissionColor", randColor);

            lastLane = offset + position;
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

    float crackDistance(scr_crack a, scr_crack b)
    {
        float xDist = Mathf.Abs(a.transform.position.x - b.transform.position.x) * crackRatio;
        float yDist = Mathf.Abs(a.transform.position.y - b.transform.position.y);
        return Mathf.Sqrt( (xDist * xDist) + (yDist * yDist) );
    }

    void makeEgg()
    {
                Vector3 eggPos = new Vector3(Random.Range(-7, 7), 4f * eggSign, 0f);
        eggSign *= -1;
        Instantiate(eggObj, eggPos, Quaternion.identity);
    }

}
