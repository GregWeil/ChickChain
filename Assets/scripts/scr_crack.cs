using UnityEngine;
using System.Collections;

public class scr_crack : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Follower")) return;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            player.GetComponent<ChickenMovement>().stun(0.5f);
        }
    }
}
