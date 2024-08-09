using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float speed = 0.1F;
	public Vector3 origin;
	public float zed = 10;
	public Ship ship;

    public float projectileSpeed = 2500f;
	public bool isFiring = false;

	void Awake() {
		Application.targetFrameRate = 60;	
	}

	// Use this for initialization
	void Start () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Fire ();
	}
		
	/// <summary>
	/// Handle move
	/// </summary>
	void Move() {

#if UNITY_IPHONE || UNITY_ANDROID
		if (Input.touchCount > 0) {
			Vector3 pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch (0).position.x, Input.GetTouch (0).position.y+1.5f, zed));
			transform.position = new Vector3 (pos.x, pos.y+1.5f, zed);
		} else {
			transform.position = origin;
		}
#else
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zed));
        transform.position = new Vector3(pos.x, pos.y, zed);
#endif	

	}

	/// <summary>
	/// Open fire
	/// </summary>
	void Fire() {
		if (ship.target != null && !isFiring) {
			StartCoroutine ("shoot");
		}
	}

	/// <summary>
	/// Shoot projectile
	/// </summary>
	/// <returns>The projectile.</returns>
	IEnumerator shoot ()
	{
		isFiring = true;
		foreach (Transform cannon in ship.cannons) {
			GameObject obj = (GameObject)Object.Instantiate (ship.bullet,cannon.position,ship.transform.rotation);
			Bullet bullet = obj.GetComponent<Bullet>();
			bullet.damage = ship.damage;
			Rigidbody rb = obj.GetComponent<Rigidbody>();

			if (ship.target != null) {
				bullet.transform.LookAt (ship.target.transform.position);
			} else {
				bullet.transform.rotation = cannon.transform.rotation;
			}


			rb.AddForce (bullet.transform.forward * projectileSpeed);
			ship.GetComponent<AudioSource>().PlayOneShot (ship.laserSound);
			yield return new WaitForSeconds(ship.reattack/ship.cannons.Length);
		}
		isFiring = false;
	}

}
