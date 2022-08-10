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
    public float velocitySpin;

    // Other components
    Camera localCamera;

    GameObject cameraCine;

    // Respawn
    bool isRespawnRequested = false;

    public Rigidbody rb;

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
        /*if (Object.HasStateAuthority)
        {
            if (isRespawnRequested)
            {
                Respawn();
                return;
            }
        }*/

        //transform.Rotate(0, 10 * Time.deltaTime * velocitySpin, 0);

        // Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Move

            Vector3 movementDirection = new Vector3(networkInputData.movementInput.x, 0, networkInputData.movementInput.y);
            rb.AddForce(movementDirection * inputForce * Runner.DeltaTime, ForceMode.Force);

            //transform.LookAt(cameraCine.transform.position);

            /*if(movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }*/

            CheckFallRespawn();
        }
    }

    void CheckFallRespawn()
    {
        if (transform.position.y < -30)
            transform.position = Utils.GetRandomSpawnPoint();
    }

    /*void CheckFallRespawn()
    {
        if (transform.position.y < -12)
        {
            if (Object.HasStateAuthority)
            {
                Debug.Log($"{Time.time} Respawn due to fall outside of map at position {transform.position}");

                Respawn();
            }
        }
    }
   
    public void RequestRespawn()
    {
        isRespawnRequested = true;
    }

    void Respawn()
    {
        CharacterMovementHandler.TeleportToPosition(Utils.GetRandomSpawnPoint());

        isRespawnRequested = false;
    }
    */

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
