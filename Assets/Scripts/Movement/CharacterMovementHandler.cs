using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    Vector2 viewInput;

    // Rotation
    public float velocity;
    public float rotationSpeed;

    // Other components
    Camera localCamera;

    GameObject cameraCine;


    Rigidbody rb;

    public float inputForce;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        cameraCine = FindObjectOfType<Camera>().gameObject;
    }

    public override void FixedUpdateNetwork()
    {
        // Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Move

            Vector3 movementDirection = new Vector3(networkInputData.movementInput.x, 0, networkInputData.movementInput.y);
            rb.AddForce(movementDirection * inputForce * Runner.DeltaTime, ForceMode.Force);

            //transform.LookAt(cameraCine.transform.position);
            
            if(movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

            //rb.AddTorque(0, networkInputData.movementInput.x * velocity, 0);

            /*
            // Jump
            if (networkInputData.isJumpPressed)
                networkCharacterControllerPrototypeCustom.Jump();
            */
        }
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
