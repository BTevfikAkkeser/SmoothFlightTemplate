using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public float radius = 25.0F;
    public float power = 100.0F;

    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 300.0F);
        }

        Object.Destroy(transform.parent.gameObject, 1.5f);
    }
}