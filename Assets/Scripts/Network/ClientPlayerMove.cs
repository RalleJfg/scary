using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using StarterAssets;

public class ClientPlayerMove : NetworkBehaviour
{
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    ThirdPersonController thirdPersonController;

    [SerializeField]
    PlayerInput playerInput;



    [SerializeField]
    Transform cameraFollow;

    void Awake()
    {
        characterController.enabled = false;
        thirdPersonController.enabled = false;
        playerInput.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;

        if(!IsOwner)  //if its not ours
        {
            enabled = false;
            characterController.enabled = false;
            thirdPersonController.enabled = false;
            playerInput.enabled = false;
            return;
        }

        
        characterController.enabled = true;
        thirdPersonController.enabled = true;
        playerInput.enabled = true;
    }
}
