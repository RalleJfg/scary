using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;


public class PlayerStatsUI : NetworkBehaviour
{
    public PlayerNetworkStats playerNetworkStats;
    public GameObject ui;
    //public Text timerText;
    public Text messageText;


    private void Awake()
    {
        if (playerNetworkStats == null)
        {
            enabled = false;
        }

        //timerText = GameObject.Find("TimeText").GetComponent<Text>();
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if (!IsOwner) return;


        //playerNetworkStats.PlayerTimeMessage += OnPlayerTimeMessageChanged;
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();


        if (!IsOwner) return;


        //playerNetworkStats.PlayerTimeMessage -= OnPlayerTimeMessageChanged;
    }


    private void Update()
    {
        if (IsOwner && !playerNetworkStats.IsDead.Value)
        {
            //timerText.text = $"Time: {playerNetworkStats.Timer.Value:F2} seconds";
        }
        if(playerNetworkStats.IsDead.Value)
        {
            //messageText.text = playerNetworkStats.PlayerTimeMessage; // Display the time message for the player

            // Access the NetworkVariable and convert it to a string
            string message = PlayerNetworkStats.instance.PlayerTimeMessage.Value.ToString();
            //Debug.Log("kkkkkkkkk" + message); // Use it for UI or other purposes
            messageText.text = message;
        }
        //messageText.text = playerNetworkStats.PlayerTimeMessage.ToString(); // Display the time message for the player
    }


    private void OnPlayerTimeMessageChanged(string oldMessage, string newMessage)
    {
        //messageText.text = playerNetworkStats.PlayerTimeMessage; // Display the time message for the player
    }


    private void OnRizzValueChanged(int previousValue, int newValue)
    {
        // Implement a visual UI change here with the UI variable
        ui.GetComponent<Text>().text = "Rizz: " + newValue;
    }
}
