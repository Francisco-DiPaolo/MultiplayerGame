using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFollow : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private float _mouseSensitivity = 1.0f;

    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget;
    [SerializeField] float BottomClamp = 70.0f;
    [SerializeField] float TopClamp = -30.0f;
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        CameraRotation();
    }
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        if (input.sqrMagnitude >= 0.01f)
        {
            //Don't multiply mouse input by Time.deltaTime;


            _cinemachineTargetYaw += input.x * _mouseSensitivity;
            _cinemachineTargetPitch += input.y * _mouseSensitivity;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch,
            _cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
