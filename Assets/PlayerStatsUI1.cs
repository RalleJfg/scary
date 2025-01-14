using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;


public class PlayerStatsUI : NetworkBehaviour
{
    public PlayerNetworkStats playerNetworkStats;
    public GameObject ui;
    public Text timerText;
    public Text messageText;


    private void Awake()
    {
        if (playerNetworkStats == null)
        {
            enabled = false;
        }
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if (!IsOwner) return;


        playerNetworkStats.PlayerTimeMessage.OnValueChanged += OnPlayerTimeMessageChanged;
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();


        if (!IsOwner) return;


        playerNetworkStats.PlayerTimeMessage.OnValueChanged -= OnPlayerTimeMessageChanged;
    }


    private void Update()
    {
        if (IsOwner && !playerNetworkStats.IsDead.Value)
        {
            timerText.text = $"Time: {playerNetworkStats.Timer.Value:F2} seconds";
        }
    }


    private void OnPlayerTimeMessageChanged(string oldMessage, string newMessage)
    {
        messageText.text = newMessage; // Display the time message for the player
    }


    private void OnRizzValueChanged(int previousValue, int newValue)
    {
        // Implement a visual UI change here with the UI variable
        ui.GetComponent<Text>().text = "Rizz: " + newValue;
    }
}
