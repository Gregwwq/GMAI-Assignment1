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

        StartCoroutine(Vanish());
    }

    void Update()
    {
        Move();

        if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
        {
            UpdateNewMovement();
        }
    }

    void Move()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);

        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void UpdateNewMovement()
    {
        minX = transform.position.x - offset;
        maxX = transform.position.x + offset;
        minZ = transform.position.z - offset;
        maxZ = transform.position.z + offset;

        targetLocation = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));

        lookRotation = Quaternion.LookRotation((targetLocation - transform.position), Vector3.up);
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
}
