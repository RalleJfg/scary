using UnityEngine;
using Unity.Netcode;
 
public class OwnerComponentManager : NetworkBehaviour
{
    [SerializeField] private Camera _camera; // This is your camera, assign it in the prefab
 
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        _camera.enabled = false;
       base.OnNetworkSpawn();
       if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
       _camera.enabled = true; // only enable YOUR PLAYER'S camera, all others will stay disabled
       
    }



    // public void Update ()
    // {
    //     if(IsOwner)
    //     {
    //         print("I am the owner");
    //     }
    //     if(IsLocalPlayer )
    //     {
    //         print("I am the owner");
    //         _camera.enabled = true;
    //     }
    // }



    
}