using UnityEngine;
using System.Collections;

public class EggPickup : MonoBehaviour {

    public GameObject chickPrefab = null;

	// Something made contact
	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Player")) return;
        if (chickPrefab != null) {
            var chick = Instantiate(chickPrefab);
            chick.transform.position = transform.position;
            var follow = chick.GetComponent<ChickFollow>();
            follow.target = col.GetComponent<ChickenPathRecord>();
        }
        Destroy(gameObject);
    }
}
