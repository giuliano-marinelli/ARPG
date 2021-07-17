using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float trajectoryLenght = 100f;
    public float expiryTime = 5f;
    public float speed = 5f;
    public float fallingThreshold = -4f;

    private Rigidbody _rigidbody;
    private float trajectoryTraveled = 0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.AddForce(transform.forward * speed);
        //Destroy(gameObject, expiryTime);
    }

    void FixedUpdate()
    {
        //trajectoryTraveled += speed * Time.fixedDeltaTime;

        //if (trajectoryLenght > trajectoryTraveled)
        //{
        //_rigidbody.velocity = transform.forward * speed * Time.fixedDeltaTime;
        //}
        //else
        //{
        //    _rigidbody.useGravity = true;
        //    if (!isFalling())
        //    {
        //        Destroy(gameObject);
        //    }
        //}

    }

    bool isFalling()
    {
        return _rigidbody.velocity.y < fallingThreshold;
    }
}
