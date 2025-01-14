using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;


public class PlayerNetworkStats : NetworkBehaviour
{
    public static PlayerNetworkStats instance;
    //public NetworkVariable<int> Rizz = new(0);


    public NetworkVariable<float> Timer = new NetworkVariable<float>(0f);
    public NetworkVariable<bool> IsDead = new NetworkVariable<bool>(false);
    public string PlayerTimeMessage = new string("");

    private float savedTimerValue = 0f;

    private bool isCounting = true;
   

   void Awake()
    {
        instance = this;
    }


    // public override void OnNetworkSpawn() {
    //     base.OnNetworkSpawn();


    //     if (!IsOwner) return;


    //     if (Rizz.Value < 15)
    //     {
    //         Debug.Log("Rizz is too low");
    //     }


    //     Rizz.OnValueChanged += OnRizzValueChanged;
    // }


    // public override void OnNetworkDespawn()
    // {
    //     base.OnNetworkDespawn();
       
    //     if (!IsOwner) return;


    //     Rizz.OnValueChanged -= OnRizzValueChanged;
    // }


    void Update()
    {
        if (IsOwner && Input.GetKeyDown(KeyCode.K)) // Press 'T' to request the timer
        {
            Debug.Log("Requesting timer value from server...");
            
            ServerTimer.instance.RequestTimerServerRpc();

            NotifyDeathServerRpc();
        }


        
    }

    // Method to save the timer value when received from the server
    public void SaveTimer(float timerValue)
    {
        savedTimerValue = timerValue;
        Debug.Log($"Timer saved: {savedTimerValue} seconds");
        // You can now use this value (e.g., display it in the UI, save it to a file, etc.)
    }


    [ServerRpc]
    private void NotifyDeathServerRpc()
    {
        
        IsDead.Value = true;
        isCounting = false;

        // Notify all clients with this player's final time
        PlayerTimeMessage = "Player " + ((int)OwnerClientId + 1) + " lost with time: " + savedTimerValue + "seconds";
        
        
        
    }


    // // === REFERENCE CODE ===
    // UnityAction DoSomethingAction;
    // System.Action Action;


    // void CallInvoke() {
    //     DoSomethingAction?.Invoke();
    // }


    // void SubscribeToCall() {
    //     DoSomethingAction += () => {};
    // }


}
