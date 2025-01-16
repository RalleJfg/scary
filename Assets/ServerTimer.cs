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
        if (IsServer) 
        {
            Timer.Value += Time.deltaTime; 
        }
        timerText.text = $"Time: {Timer.Value:F1} seconds";
    }

    
    [ServerRpc(RequireOwnership = false)]
    public void RequestTimerServerRpc(ServerRpcParams rpcParams = default)
    {
        RespondTimerClientRpc(Timer.Value, rpcParams.Receive.SenderClientId);
    }

    
    [ClientRpc]
    private void RespondTimerClientRpc(float timerValue, ulong clientId)
    {
        PlayerNetworkStats.instance.SaveTimer(timerValue);
    }
}
