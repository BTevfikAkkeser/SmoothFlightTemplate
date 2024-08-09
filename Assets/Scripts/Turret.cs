using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject ship;
	public float speed = 5f;
	public float reattack = 1f;
	public float damage = 1f;
    public float projectileSpeed = 1000f;

	public Transform cannon;
	public GameObject projectile;

	// Use this for initialization
	void Start () {
		ship = GameObject.FindGameObjectWithTag ("Ship");
        InvokeRepeating ("FireLaser", reattack, reattack);
	}

	// Update is called once per frame
	void Update () {
	
		var targetRotation = Quaternion.LookRotation(ship.transform.position - transform.position);

		// Smoothly rotate towards the target point.
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

	}

	void FireLaser() {
		Vector3 tar = ship.transform.position - transform.position;

		var targetRotation = Quaternion.LookRotation(tar);

		GameObject obj = (GameObject)Object.Instantiate (projectile,cannon.position,targetRotation);
		//Add minimum variance in aim to laser rotation 
		Bullet bullet = obj.GetComponent<Bullet>();
		bullet.damage = damage;
		Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.AddForce (bullet.transform.forward * projectileSpeed);
	}
}
