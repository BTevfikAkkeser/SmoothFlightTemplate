using UnityEngine;
using System.Collections;

public class ShipCannon : MonoBehaviour {

	public GameObject effect;
	public ShipController shipController;

	private float _flashDuration = 0.085f;
	private bool _isRotating = false;

	// Use this for initialization
	void Start () {
		effect.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (shipController.isFiring && !_isRotating) {
			transform.eulerAngles = new Vector3 (0, 0, Random.Range (0, 360));
			int scale = Random.Range (2, 5);
			transform.localScale = new Vector3 (scale / 2, scale / 2, scale / 2);
			StartCoroutine("rotate");
		}
	}

	IEnumerator rotate () {
		_isRotating = true;
		effect.SetActive(true);
        yield return new WaitForSeconds(_flashDuration);
		effect.SetActive(false);
        yield return new WaitForSeconds(shipController.ship.reattack-_flashDuration);
		_isRotating = false;
	}
}
