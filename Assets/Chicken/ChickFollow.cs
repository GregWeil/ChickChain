using UnityEngine;
using System.Collections;

public class ChickFollow : MonoBehaviour {

    public ChickenPathRecord target = null;
    public float distance = 1f;

    Rigidbody2D body = null;

    Vector2 speed = Vector2.zero;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
    }

    // Called every physics step
    void FixedUpdate() {
        Vector2 position = target.pathPosition(distance);
        body.MovePosition(Vector2.SmoothDamp(transform.position, position, ref speed, 0.05f));
    }

    // Update is called once per frame
    void Update() {

    }
}
