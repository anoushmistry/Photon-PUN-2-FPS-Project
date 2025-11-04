using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RoomManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;

    void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
        //base.OnConnectedToMaster();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom("Room1", null, null);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
       

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);

        Debug.Log("Player Spawned");
    }
}

