using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Cinemachine;
using TMPro;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public TextMeshProUGUI playerNicknameTM;
    public static NetworkPlayer Local { get; set; }
    public Transform playerModel;

    [Header ("Colores")]
    public Color[] colores;

    [Networked] public int idColor { get; set; }

    public GameObject spinning;

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

    bool isPublicJoinMessageSent = false;

    public GameObject localUI;

    //Other components
    NetworkInGameMessages networkInGameMessages;

    void Awake()
    {
        networkInGameMessages = GetComponent<NetworkInGameMessages>();
    }

    void Start()
    {
        if (Object.HasInputAuthority)
        {
            GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().Follow = transform.GetChild(0);
            GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().LookAt = transform.GetChild(0);
        }

        ChangeId();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local player");

            // Sets the layer of the local player model

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickName"));
        }
        else
        { 
            Debug.Log("Spawned remote player");

            //Disable UI for remote player
            localUI.SetActive(false);
        }

        // Set the player as a player is  which
        Runner.SetPlayerObject(Object.InputAuthority, Object);

        // Make it easier to tell which player is which
        transform.name = $"P_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (Object.HasStateAuthority)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject playerLeftNetworkObject))
            {
                if (playerLeftNetworkObject == Object)
                    Local.GetComponent<NetworkInGameMessages>().SendInGameRPCMessage(playerLeftNetworkObject.GetComponent<NetworkPlayer>().nickName.ToString(), "left");
                
                //networkInGameMessages.SendInGameRPCMessage(nickName.ToString(), "left");
            }
        }

        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nick name changed for player to {nickName} for player {gameObject.name}");

        playerNicknameTM.text = nickName.ToString();
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;

        if (!isPublicJoinMessageSent)
        {
            networkInGameMessages.SendInGameRPCMessage(nickName, "joined");

            isPublicJoinMessageSent = true;
        }
    }

    public void ChangeColor(int color)
    {
        spinning.GetComponent<MeshRenderer>().material.color = colores[color];

    }

    public void ChangeId()
    {
        if (Object.HasInputAuthority)
        {
            idColor = FindObjectOfType<Spawner>().id;
        }

        ChangeColor(idColor);
    }
}
