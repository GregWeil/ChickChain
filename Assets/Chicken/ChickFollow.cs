using UnityEngine;
using System.Collections;

public class ChickFollow : MonoBehaviour {

    public ChickenPathRecord target = null;

    Rigidbody2D body = null;

    Vector2 posTarget = Vector2.zero;
    float posGround = 0f;
    float distance = 1f;

    Vector2 speed = Vector2.zero;
    float speedV = 0f;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        posTarget = body.position;
        posGround = transform.position.z;
        distance = target.reserveSlot();
    }

    // Called every physics step
    void FixedUpdate() {
        Vector2 position = target.pathPosition(distance);
        if (Vector2.Distance(posTarget, position) > 0.5f) {
            if ((transform.position.z <= posGround) && (speedV <= 0f)) {
                posTarget = Vector2.MoveTowards(posTarget, position, 0.75f);
                speedV += Random.Range(10f, 20f);
            }
        }
        body.MovePosition(Vector2.SmoothDamp(transform.position, posTarget, ref speed, 0.05f));
        if (speed.sqrMagnitude > 0.5f) {
            var angle = (Mathf.Atan2(speed.y, speed.x) * Mathf.Rad2Deg);
            body.MoveRotation(Mathf.MoveTowardsAngle(body.rotation, angle, (1000f * Time.fixedDeltaTime)));
        }
    }

    // Update is called once per frame
    void Update() {
        var position = transform.position;
        speedV -= (200f * Time.deltaTime);
        position.z += (speedV * Time.deltaTime);
        if (position.z < posGround) {
            position.z = posGround;
            speedV = 0f;
        }
        transform.position = position;
    }
}
