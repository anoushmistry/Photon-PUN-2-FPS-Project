using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        playerMovement.enabled = true;
        playerCamera.SetActive(true);
    }
}
