using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class XHairController : MonoBehaviour {

	public Ship ship;
	public Transform destination;
	public float speed=4f;
	public bool allowTargeting = false;

	public static List<GameObject> targets = new List<GameObject>();

	void Update() {

		transform.position =   Vector3.Lerp(transform.position, destination.position, Time.deltaTime*speed*2f);
		transform.rotation =   Quaternion.Lerp (transform.rotation, destination.rotation, Time.deltaTime*speed);

		if (allowTargeting) {
			Vector3 dir = transform.position - Camera.main.transform.position;

			RaycastHit hit;
			ship.target = null;
			if (Physics.Raycast (transform.position, dir, out hit)) {
				if (hit.distance <= ship.radarRange) {

					if (hit.transform.gameObject.CompareTag ("Enemy")) {
						ship.target = hit.transform.gameObject;
						AddTarget (hit.transform.gameObject);
					}
				}
			}
		}
	}

	public static void AddTarget(GameObject obj) {
		if (!targets.Contains (obj)) {
			targets.Add (obj);
		}	
	}

	public static void RemoveTarget(GameObject obj) {
		targets.Remove (obj);
	}
}
