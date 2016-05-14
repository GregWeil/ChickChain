using UnityEngine;
using System.Collections;

public class scr_crack : MonoBehaviour {

    private Renderer crackMat;
    public Material baseMaterial;
    public Material hitMaterial;

    private float timer = 0f;

    void Start()
    {
        crackMat = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                crackMat.material = baseMaterial;
            }
        }
    }

	void OnTriggerEnter2D (Collider2D col) {
        if (!col.CompareTag("Follower")) return;
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            player.GetComponent<ChickenMovement>().stun(0.5f);
            crackMat.material = hitMaterial;
            timer = 0.5f;
        }
    }
}
