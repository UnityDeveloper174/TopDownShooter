using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f; // Speed of the bullet
    public float lifetime = 2f; // Lifetime of the bullet in seconds
    [NonSerialized] public float damage;

    void Start()
    {
        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            //collision.transform.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }


}
