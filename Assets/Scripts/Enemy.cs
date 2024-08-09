using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public float limitZ;
	public float speedRate=0.75f;

	public float hp = 5f;
	public float points = 5f;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		speedRate = Random.Range (1, 4);
		limitZ = GameObject.FindGameObjectWithTag("Player").transform.position.z;
		transform.position = new Vector3 (Random.Range (-20, 20), 0, 255);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(-Vector3.forward*speedRate);
		if (limitZ - transform.position.z >= 10) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.transform.tag == "Bullet") {
			Bullet bullet = collision.transform.gameObject.GetComponent<Bullet>();
			hp = hp - bullet.damage;
			if (hp <= 0) {
                Instantiate(explosion, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		}
	}

    void OnDestroy()
    {
        XHairController.RemoveTarget(gameObject);
    }
}
