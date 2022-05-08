using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotate : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    [SerializeField] private float LeftPushRange;
    [SerializeField] private float RightPushRange;
    [SerializeField] private float velocityThreshold;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.angularVelocity = velocityThreshold;
    }

    void FixedUpdate()
    {
        if (transform.rotation.z > 0 && transform.rotation.z <
           RightPushRange && (rigidBody.angularVelocity > 0)
           && rigidBody.angularVelocity < velocityThreshold)
        {
            rigidBody.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z >
        LeftPushRange && (rigidBody.angularVelocity < 0)
        && rigidBody.angularVelocity > velocityThreshold * -1f)
        {
            rigidBody.angularVelocity = velocityThreshold * -1f;
        }
    }
}
