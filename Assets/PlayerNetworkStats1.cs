using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
using System.Collections;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerNetworkStats : NetworkBehaviour
{
    public static PlayerNetworkStats instance;
    //public NetworkVariable<int> Rizz = new(0);


    //public NetworkVariable<float> Timer = new NetworkVariable<float>(0f);
    public NetworkVariable<bool> IsDead = new NetworkVariable<bool>(false);


    //public string PlayerTimeMessage = new string("");

    public NetworkVariable<FixedString64Bytes> PlayerTimeMessage = new NetworkVariable<FixedString64Bytes>("");

    private float savedTimerValue = 0f;

    private bool isCounting = true;

    public Text joinedText;
   

   void Awake()
    {
        instance = this;

        joinedText = GameObject.Find("JoinedText").GetComponent<Text>();
    }


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            // Broadcast a message to all clients when a new player joins
            NotifyPlayerJoinedServerRpc(OwnerClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void NotifyPlayerJoinedServerRpc(ulong clientId)
    {
        // Notify all clients
        NotifyPlayerJoinedClientRpc(clientId);
    }

    [ClientRpc]
    private void NotifyPlayerJoinedClientRpc(ulong clientId)
    {
        // Display a message on all clients
        //Debug.Log($"Player {((int)clientId + 1)} has joined the game!");
        joinedText.text = $"Player {((int)clientId + 1)} has joined the game!";

        StartCoroutine(Delay(2f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Press 'K' to request the Death
        {
            Debug.Log("Requesting timer value from server...");
            
            if (ServerTimer.instance != null)
            {
                if (ServerTimer.instance == null)
                {
                    Debug.LogError("ServerTimer instance is not available on the client.");
                }
                else
                {
                    ServerTimer.instance.RequestTimerServerRpc();
                }
            }

            NotifyDeathServerRpc();
        }


        
    }

    // Method to save the timer value when received from the server
    public void SaveTimer(float timerValue)
    {
        savedTimerValue = timerValue;
        
    }


    [ServerRpc(RequireOwnership = false)]
    public void NotifyDeathServerRpc()
    {
        PlayerTimeMessage.Value = new FixedString64Bytes("Player " + ((int)OwnerClientId + 1) + " lost with time: " + savedTimerValue + "seconds");

        
        IsDead.Value = true;
        isCounting = false;
        
    }

    private IEnumerator Delay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        joinedText.text = "";
    }

}
