using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 7;
    public float smoothMoveTime = .1f; // The amount of time to catch up to the target input magnitude
    public float turnSpeed = 8;

    float angle;
    float smoothInputMagnitude;
    float smoothMoveVelocity; // velocity to track
    Vector3 velocity;

    Rigidbody rigidBody;
    bool disabled;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        Guard.OnGuardHasSpottedPlayer += Disable;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputDirection = Vector3.zero;
        if (!disabled)
            inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);
        //transform.eulerAngles = Vector3.up * angle;

        //transform.Translate(transform.forward * moveSpeed * Time.deltaTime * smoothInputMagnitude, Space.World);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }

    void Disable()
    {
        disabled = true;
    }

    private void FixedUpdate()
    {
        rigidBody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
    }

    private void OnDestroy()
    {
        Guard.OnGuardHasSpottedPlayer -= Disable;
    }
}
