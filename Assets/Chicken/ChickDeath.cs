using UnityEngine;
using System.Collections;

public class ChickDeath : MonoBehaviour {

    public GameObject effectPrefab = null;

	void Kill () {
        var effect = Instantiate(effectPrefab);
        effect.name = effectPrefab.name;
        effect.transform.position = transform.position;
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

}
