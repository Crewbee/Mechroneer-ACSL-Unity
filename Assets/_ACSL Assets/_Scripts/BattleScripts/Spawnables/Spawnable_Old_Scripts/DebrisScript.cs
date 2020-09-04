using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour, ISpawnableObject
{
    public float upForce = 4.0f;

    public float sideForce = 6.0f;

    public float Lifetime = 8.0f;

    private void Update()
    {
        Lifetime -= Time.deltaTime;

        if(Lifetime <= 0.0f) // When life time is done 
        {
            Lifetime = 8.0f;

            gameObject.SetActive(false); //Return to inactive state
        }
    }

    public void OnObjectSpawn() //Creates a random force vector and applies it to the rigid body on spawn
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 direction = Quaternion.Euler(0, (Random.Range(0.0f, 360.0f)), 0)
            * (Vector3.forward * xForce)
            + (Vector3.up * yForce);

        direction.Normalize();

        Vector3 force = direction * ((upForce + sideForce) / 2.0f);

        GetComponent<Rigidbody>().velocity = force;

        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
