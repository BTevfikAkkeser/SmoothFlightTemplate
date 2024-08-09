using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;

    public float frequencySeconds;

	void Start () {
        InvokeRepeating("SpawnItem", frequencySeconds, frequencySeconds);
	}

    void SpawnItem () {
        GameObject prefab = (GameObject)prefabs.GetValue(Random.Range(0, prefabs.Length - 1));
        Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
	}
}
