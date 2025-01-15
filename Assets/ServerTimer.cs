using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class ServerTimer : NetworkBehaviour
{
    public static ServerTimer instance;
    public NetworkVariable<float> Timer = new NetworkVariable<float>(0f);

    public Text timerText;
    //public Text messageText;
    
    


    private void Awake()
    {
        instance = this;

        timerText = GameObject.Find("TimeText").GetComponent<Text>();
        //messageText = GameObject.Find("MessageText").GetComponent<Text>();
    }
    private void Update()
    {
        if (IsServer) // Only run on the server
        {
            Timer.Value += Time.deltaTime; // Increment timer each frame on the server
            
        }

        timerText.text = $"Time: {Timer.Value:F2} seconds";
    }

    
    [ServerRpc(RequireOwnership = false)]
    public void RequestTimerServerRpc(ServerRpcParams rpcParams = default)
    {
        // Send the current timer value to the requesting client
        
        RespondTimerClientRpc(Timer.Value, rpcParams.Receive.SenderClientId);
    }

    
    [ClientRpc]
    private void RespondTimerClientRpc(float timerValue, ulong clientId)
    {

        PlayerNetworkStats.instance.SaveTimer(timerValue);

    }
}
