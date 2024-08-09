using UnityEngine;
using System.Collections;

public enum Motion { Bouncy, Linear };

public class Ship : MonoBehaviour {

	public Motion motionType = Motion.Bouncy;
	public Transform marker;
	public Transform[] cannons;
	public GameObject bullet;
	public GameObject target;

	[Range(10f, 300f)]
	public float radarRange = 300f;

	[Range(1f, 10f)]
	public float moveConstant = 10f;

	[Range(1f, 10f)]
	public float rotateConstant = 10f;

	public AudioClip laserSound;

	//Attack stats
	public float damage = 1f;
	public float reattack = 0.5f;
	public float maxPower = 50f;
	public float power = 50f;

	private Rigidbody _rb;

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 destinationDistanceDelta = marker.position - transform.position;
		float angleY = Mathf.LerpAngle(transform.eulerAngles.y, marker.position.x, Time.time);
		float angleX = Mathf.LerpAngle(transform.eulerAngles.x, -marker.position.y, Time.time);

		switch (motionType)
		{
			case Motion.Linear:
				MoveLinear(destinationDistanceDelta, angleX, angleY);
				break;
			case Motion.Bouncy:
			default:
				MoveBouncy(destinationDistanceDelta, angleX, angleY);
				break;
		}
	}

	void MoveBouncy(Vector3 delta, float angleX, float angleY) 
    {
		_rb.AddForce(delta * moveConstant, ForceMode.Acceleration);
		transform.eulerAngles = new Vector3(angleX, angleY, -rotateConstant * _rb.velocity.x);
	}

	void MoveLinear(Vector3 delta, float angleX, float angleY)
    {
        transform.position = Vector3.MoveTowards(transform.position, marker.position, moveConstant * Time.deltaTime * delta.magnitude);
		transform.eulerAngles = new Vector3(angleX, angleY, -rotateConstant * delta.x);
	}
}
