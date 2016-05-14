using UnityEngine;
using System.Collections;

public class ChickenMovement : MonoBehaviour {

    public GameObject mesh = null;
    public AudioSource bokSound = null;
    public AudioSource stunSound = null;

    Rigidbody2D body;

    Vector2 movement = Vector2.zero;
    float speed = 2.5f;
    float accel = 15f;

    float posGround = 0f;
    float speedV = 0f;

    float stunTime = 0f;
    float threshold = 0.5f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        posGround = transform.position.z;
	}

    // Called every physics step
    void FixedUpdate () {
        body.velocity = Vector2.MoveTowards(body.velocity, (movement * speed), (accel * Time.fixedDeltaTime));
        if (movement.magnitude > threshold) {
            var angle = (Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg);
            body.MoveRotation(Mathf.MoveTowardsAngle(body.rotation, angle, (300f * Time.fixedDeltaTime)));
        }
    }
	
	// Update is called once per frame
	void Update () {
        movement = new Vector2(-Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (movement.sqrMagnitude > 1) movement.Normalize();
        if (movement.magnitude < threshold) movement = Vector2.zero;
        if (!scr_road.gameStart) movement = Vector2.zero;

        stunTime -= Time.deltaTime;
        if (stunTime > 0f) {
            movement = Vector2.zero;
            mesh.transform.localPosition = (0.1f * Random.value * (Quaternion.AngleAxis((Random.value * 360f), Vector3.forward) * Vector3.right));
        } else {
            mesh.transform.localPosition = Vector3.zero;
        }

        var position = transform.position;
        if ((position.z <= posGround) && (speedV <= 0)) {
            if ((movement.magnitude > threshold) || (Input.GetButtonDown("Jump") && scr_road.gameStart)) {
                speedV += 5f;
                bokSound.pitch = Random.Range(0.9f, 1.1f);
                bokSound.Play();
            }
        }
        if (Input.GetButton("Jump") && (speedV > 0f)) speedV += (5f * Time.deltaTime);
        speedV -= (30f * Time.deltaTime);
        position.z += (speedV * Time.deltaTime);
        if (position.z < posGround) {
            position.z = posGround;
            speedV = 0f;
        }
        transform.position = position;
	}

    public void stun(float time) {
        stunTime = Mathf.Max(time, stunTime);
        stunSound.Play();
    }
}
