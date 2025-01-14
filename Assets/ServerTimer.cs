using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class ServerTimer : NetworkBehaviour
{
    public static ServerTimer instance;
    public NetworkVariable<float> Timer = new NetworkVariable<float>(0f);

    public Text timerText;

    private void Awake()
    {
        instance = this;

        timerText = GameObject.Find("TimeText").GetComponent<Text>();
    }
    private void Update()
    {
        if (IsServer) // Only run on the server
        {
            Timer.Value += Time.deltaTime; // Increment timer each frame on the server
            //Debug.Log($"Server Timer: {Timer.Value}");
            timerText.text = $"Time: {Timer.Value:F2} seconds";
        }
    }

    // ServerRpc to allow clients to request the current timer value
    [ServerRpc]
    public void RequestTimerServerRpc(ServerRpcParams rpcParams = default)
    {
        // Send the current timer value to the requesting client
        
        RespondTimerClientRpc(Timer.Value, rpcParams.Receive.SenderClientId);
    }

    // ClientRpc to send the current timer value to the client
    [ClientRpc]
    private void RespondTimerClientRpc(float timerValue, ulong clientId)
    {
        // Check if this is the correct client receiving the value
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            Debug.Log($"Received Timer value from server: {timerValue}");
            // Handle the timer value on the client side (e.g., update UI)

            PlayerNetworkStats.instance.SaveTimer(timerValue);
        }
    }
}
