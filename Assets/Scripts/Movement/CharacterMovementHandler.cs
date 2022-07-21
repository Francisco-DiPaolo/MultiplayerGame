using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    Vector2 viewInput;

    // Rotation
    float cameraRotationX = 0;

    // Other components
    Camera localCamera;

    Rigidbody rb;

    public float inputForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //localCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*cameraRotationX += viewInput.y * Time.deltaTime;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90, 90);

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);*/
    }

    public override void FixedUpdateNetwork()
    {
        // Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Move
            rb.AddForce(new Vector3 (networkInputData.movementInput.x, 0, networkInputData.movementInput.y) * inputForce * Runner.DeltaTime, ForceMode.Force);
            
            // Rotate the view


            /*
            // Move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

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
