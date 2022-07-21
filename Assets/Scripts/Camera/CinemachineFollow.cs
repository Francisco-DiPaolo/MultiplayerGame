using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineFollow : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    private GameObject tPlayer;
    private Transform tFollowTarget;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        if (tPlayer == null)
        {
            tPlayer = GameObject.FindWithTag("Player");
        }

        tFollowTarget = tPlayer.transform;

        vcam.LookAt = tFollowTarget;
        vcam.Follow = tFollowTarget;
    }
}
