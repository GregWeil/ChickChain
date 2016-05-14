using UnityEngine;
using System.Collections;

public class EggPickup : MonoBehaviour {

    public GameObject chickPrefab = null;
    public GameObject effectPrefab = null;

	// Something made contact
	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Player")) return;
        if (effectPrefab != null) {
            var effect = Instantiate(effectPrefab);
            effect.transform.position = transform.position;
            Destroy(effect, 5f);
        }
        if (chickPrefab != null) {
            var chick = Instantiate(chickPrefab);
            chick.name = chickPrefab.name;
            chick.transform.position = transform.position;
            var follow = chick.GetComponent<ChickFollow>();
            follow.target = col.GetComponent<ChickenPathRecord>();
        }
        Destroy(gameObject);
    }
}
