using UnityEngine;
using System.Collections;

public class scr_crack : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Follower")) return;
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ChickenMovement>().stun(0.5f);
    }
}
