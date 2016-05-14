using UnityEngine;
using System.Collections;

public class EggPickup : MonoBehaviour {

    public GameObject chickPrefab = null;
    public AudioSource sound = null;

	// Something made contact
	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Player")) return;
        sound.transform.parent = null;
        Destroy(sound.gameObject, 5f);
        sound.Play();
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
