using UnityEngine;
using System.Collections;

public class scr_coin : MonoBehaviour {

    public AudioSource sound = null;
    public AudioSource clink = null;

    float acceleration = 5f;
    float velocity = 0f;
    int numBounces = 0;

    private scr_camera cameraObj;
    private scr_road roadObj;

    // Something made contact
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) {
            Destroy(sound.gameObject, 5f);
            sound.transform.parent = null;
            sound.Play();
            scr_car[] allCars = FindObjectsOfType<scr_car>();
            for (var i = 0; i < allCars.Length; i++)
            {
                Destroy(allCars[i].gameObject);     
            }
            Destroy(gameObject);
            roadObj.makeCracks();
            cameraObj.Shake();
        }
        
    }

    void Start()
    {
        cameraObj = Camera.main.GetComponent<scr_camera>();
        roadObj = FindObjectOfType<scr_road>();
        clink = GetComponent<AudioSource>();
    }

    void Update()
    {
        velocity -= acceleration * Time.deltaTime;    

        if (transform.position.z < 0)
        {
            if (numBounces >= 3) {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                acceleration = 0;
                velocity = 0;
            }
            else {
                if (numBounces == 0) { clink.Play(); clink.pitch = 0.8f; }
                else if (numBounces == 1) { clink.Play(); clink.pitch = 0.5f; }
                else if (numBounces == 2) { clink.Play(); }

                velocity *= -0.3f;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
                numBounces++;
            }  
        }

        transform.Translate(0f, 0f, velocity * Time.deltaTime);

    }
}
