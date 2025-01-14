using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;


public class PlayerNetworkStats : NetworkBehaviour
{
    public NetworkVariable<int> Rizz = new(0);


    public NetworkVariable<float> Timer = new NetworkVariable<float>(0f);
    public NetworkVariable<bool> IsDead = new NetworkVariable<bool>(false);
    public NetworkVariable<string> PlayerTimeMessage = new NetworkVariable<string>("");


    private bool isCounting = true;
   


    public override void OnNetworkSpawn() {
        base.OnNetworkSpawn();


        if (!IsOwner) return;


        if (Rizz.Value < 15)
        {
            Debug.Log("Rizz is too low");
        }


        Rizz.OnValueChanged += OnRizzValueChanged;
    }


    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
       
        if (!IsOwner) return;


        Rizz.OnValueChanged -= OnRizzValueChanged;
    }


    void Update()
    {
        if (IsOwner && !IsDead.Value && isCounting)
        {
            Timer.Value += Time.deltaTime; // Increment the timer for this player
        }


        // Example input for testing "death"
        if (IsOwner && Input.GetKeyDown(KeyCode.K)) // Press 'K' to simulate death
        {
            NotifyDeathServerRpc();
        }
    }


    [ServerRpc]
    private void NotifyDeathServerRpc()
    {
        IsDead.Value = true;
        isCounting = false;


        // Notify all clients with this player's final time
        PlayerTimeMessage.Value = $"Player {OwnerClientId} finished with time: {Timer.Value} seconds";
    }


    public void OnRizzValueChanged(int oldValue, int newValue)
    {
        Debug.Log("NewValue of Rizz: " + newValue);
    }


    [ServerRpc]
    private void ChangeRizzServerRpc() {
        // This is the server doing things


        Rizz.Value = 15;
    }




    // === REFERENCE CODE ===
    UnityAction DoSomethingAction;
    System.Action Action;


    void CallInvoke() {
        DoSomethingAction?.Invoke();
    }


    void SubscribeToCall() {
        DoSomethingAction += () => {};
    }


}
