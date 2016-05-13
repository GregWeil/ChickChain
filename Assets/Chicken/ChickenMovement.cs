using UnityEngine;
using System.Collections;

public class ChickenMovement : MonoBehaviour {

    Rigidbody2D body;

    Vector2 movement = Vector2.zero;
    float speed = 5.0f;
    float accel = 25.0f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}

    // Called every physics step
    void FixedUpdate () {
        body.velocity = Vector2.MoveTowards(body.velocity, (movement * speed), (accel * Time.fixedDeltaTime)); ;
    }
	
	// Update is called once per frame
	void Update () {
        movement = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.sqrMagnitude > 1) movement.Normalize();
	}
}
