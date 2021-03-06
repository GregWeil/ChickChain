﻿using UnityEngine;
using System.Collections;

public class ChickFollow : MonoBehaviour {

    public ChickenPathRecord target = null;
    public AudioSource chirpSound = null;

    ChickFollow trailing = null;
    float trailingTime = 0.0f;
    public float distance = 1f;

    Rigidbody2D body = null;

    Vector2 posTarget = Vector2.zero;
    float posGround = 0f;

    Vector2 speed = Vector2.zero;
    float speedV = 0f;


	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        posTarget = body.position;
        posGround = transform.position.z;
        FindTrailingTarget();
        trailingTime = Time.time;
        chirpSound.pitch = Random.Range(0.8f, 1.2f);
    }

    // Called every physics step
    void FixedUpdate() {
        Vector2 position = posTarget;
        if (target != null) {
            position = target.pathPosition(distance);
        } else {
            position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        }
        if (Vector2.Distance(posTarget, position) > 0.5f) {
            if ((transform.position.z <= posGround) && (speedV <= 0f)) {
                //Do a hop
                posTarget = Vector2.MoveTowards(posTarget, position, 0.75f);
                speedV += Random.Range(10f, 20f);
                chirpSound.Play();
                //Move up if the next spot is open
                if (Time.time > trailingTime) {
                    if (trailing == null) {
                        FindTrailingTarget(distance);
                    } else if ((trailing != null) && (trailing != this)) {
                        distance = (trailing.distance + 1f);
                    }
                }
            }
        }
        body.MovePosition(Vector2.SmoothDamp(transform.position, posTarget, ref speed, 0.05f));
        if (speed.sqrMagnitude > 0.5f) {
            var angle = (Mathf.Atan2(speed.y, speed.x) * Mathf.Rad2Deg);
            body.MoveRotation(Mathf.MoveTowardsAngle(body.rotation, angle, (1000f * Time.fixedDeltaTime)));
        }
        if (trailing != null) {
            if ((trailing == this) || (Mathf.Abs((trailing.distance + 1f) - distance) < 0.1f)) {
                trailingTime = (Time.time + 1f);
            }
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

    void FindTrailingTarget (float oldDistance = float.MaxValue) {
        distance = -0.3f;
        foreach (var chick in GameObject.FindObjectsOfType<ChickFollow>()) {
            if (chick.target != target) continue;
            float chickDistance = (chick.distance + 1f);
            if ((chickDistance > distance) && (chickDistance <= oldDistance)) {
                distance = chickDistance;
                trailing = chick;
            }
        }
    }
}
