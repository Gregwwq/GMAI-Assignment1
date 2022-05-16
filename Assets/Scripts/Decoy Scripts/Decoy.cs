using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Decoy : MonoBehaviour
{
    Vector3 targetLocation;
    Quaternion lookRotation;

    float speed = 2f;
    float rotationSpeed = 200f;
    float offset = 3f;
    float minX, maxX, minZ, maxZ;

    void Start()
    {
        targetLocation = transform.position;
        lookRotation = transform.rotation;

        // make the decoy disappear after a period of time
        StartCoroutine(Vanish());
    }

    void Update()
    {
        // moving the decoy to the target location
        Move();

        // if the decoy has reached its target location, generate a new one
        if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
        {
            UpdateNewMovement();
        }
    }

    // function to move the decoy to its target location
    void Move()
    {
        // moving towards the target rotation
        transform.position =
            Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);

        // smoothly rotating towards the target rotation
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void UpdateNewMovement()
    {
        // setting the boundaries of the new random location
        minX = transform.position.x - offset;
        maxX = transform.position.x + offset;
        minZ = transform.position.z - offset;
        maxZ = transform.position.z + offset;

        // setting the new random target location with the boundaries
        targetLocation = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));

        // setting the target rotation to face the target location
        lookRotation = Quaternion.LookRotation((targetLocation - transform.position), Vector3.up);
    }

    // coroutine to destroy the gameobject after 6 seconds
    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
    }
}
