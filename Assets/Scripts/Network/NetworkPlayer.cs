using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Cinemachine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }

    void Start()
    {
        if (Object.HasInputAuthority)
        {
            GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0);
            GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().LookAt = transform.GetChild(0);
        }
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;

            Debug.Log("Spawned local player");
        }
        else Debug.Log("Spawned remote player");
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
}
