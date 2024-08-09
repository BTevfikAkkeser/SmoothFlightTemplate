using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public float limitZ;
	public float speedRate=0.75f;

	void Start () {
		limitZ = GameObject.FindGameObjectWithTag("Player").transform.position.z;
		speedRate = Random.Range (1, 10);
		float scale = Random.Range (1, 10);
		transform.localScale = new Vector3 (scale,scale,scale);
		transform.position = new Vector3 (Random.Range (-40, 40), Random.Range(0,40), Random.Range(250,300));
	}
	
	void Update () {
		transform.Translate(-Vector3.forward*speedRate);

		if (limitZ - transform.position.z >= 10) {
			Destroy (gameObject);
		}
	}

}
