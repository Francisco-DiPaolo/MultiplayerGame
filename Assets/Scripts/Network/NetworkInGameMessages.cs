using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkInGameMessages : NetworkBehaviour
{
    InGameMassageUIHandler inGameMassageUIHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SendInGameRPCMessage(string userNickName, string message)
    {
        RPC_InGameMessage($"<b>{userNickName}</b> {message}");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]

    void RPC_InGameMessage(string message, RpcInfo info = default)
    {
        Debug.Log($"[RPC] InGameMessage {message}");

        if (inGameMassageUIHandler == null)
            inGameMassageUIHandler = NetworkPlayer.Local.GetComponentInChildren<InGameMassageUIHandler>();

        if (inGameMassageUIHandler != null)
            inGameMassageUIHandler.OnGameMessageReceived(message);
    }
}
