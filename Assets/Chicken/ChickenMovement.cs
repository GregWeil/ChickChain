using UnityEngine;
using System.Collections;

public class ChickenMovement : MonoBehaviour {

    Rigidbody2D body;

    Vector2 movement = Vector2.zero;
    float speed = 5.0f;
    float accel = 25.0f;

    float posGround = 0f;
    float speedV = 0f;

    float stunTime = 0f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        posGround = transform.position.z;
	}

    // Called every physics step
    void FixedUpdate () {
        body.velocity = Vector2.MoveTowards(body.velocity, (movement * speed), (accel * Time.fixedDeltaTime)); ;
    }
	
	// Update is called once per frame
	void Update () {
        stunTime -= Time.deltaTime;
        movement = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.sqrMagnitude > 1) movement.Normalize();
        if (stunTime > 0f) movement = Vector2.zero;

        var position = transform.position;
        if ((position.z <= posGround) && (speedV <= 0)) {
            if (movement.magnitude > 0.1f) {
                speedV += 7.5f;
            }
        }
        speedV -= (50f * Time.deltaTime);
        position.z += (speedV * Time.deltaTime);
        if (position.z < posGround) {
            position.z = posGround;
            speedV = 0f;
        }
        transform.position = position;
	}

    public void stun(float time) {
        stunTime = Mathf.Max(time, stunTime);
    }
}
