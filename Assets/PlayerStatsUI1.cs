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

    public string message;
    


    private void Awake()
    {
        if (playerNetworkStats == null)
        {
            enabled = false;
        }

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
        if(playerNetworkStats.IsDead.Value)
        {
            message = PlayerNetworkStats.instance.PlayerTimeMessage.Value.ToString();
            
            messageText.text = message;
        }
        
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
