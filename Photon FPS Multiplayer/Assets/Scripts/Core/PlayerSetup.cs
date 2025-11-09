using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject playerCamera;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void SetupLocalPlayer()
    {
        playerMovement.enabled = PhotonView.Get(this).IsMine;
        playerCamera.SetActive(PhotonView.Get(this).IsMine);
    }
}
