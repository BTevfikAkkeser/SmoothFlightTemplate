using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	public float damage =10f;

	// Use this for initialization
	void Start () {
		GameObject.Destroy (gameObject, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
//		GameObject.Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
