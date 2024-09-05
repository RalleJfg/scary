using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
using Unity.Services.Authentication;
using System;

public class PlayerSettings : NetworkBehaviour
{
    public GMS gms;
    [SerializeField] public string name = "fddfg";
    
    
    [SerializeField] public TextMeshProUGUI playerName;
    [SerializeField] private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>(
        "Player: 0", NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    [SerializeField] public NetworkVariable<int> age = new NetworkVariable<int>();
        

    //public NetworkVariable<FixedString128Bytes> user = new NetworkVariable<FixedString128Bytes>(new FixedString128Bytes()
    //{
    //    Uname = ""
    //});

    //public struct NetString : INetworkSerializable, System.IEquatable<NetString>
    //{
     //   public string Uname;

      //  public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
      //  {
       //     if(serializer.IsReader)
       //     {
        //        var reader = serializer.GetFastBufferReader();
        //        reader.ReadValueSafe(out Uname);
        //    }
        //    else
        //    {
       //         var writer = serializer.GetFastBufferWriter();
        //        writer.WriteValueSafe(Uname);
        //    }
       // }
       // public bool Equals(NetString other)
       // {
       //     if(string.Equals(other.Uname, Uname, StringComparison.CurrentCultureIgnoreCase))
        //        return true;
        //    return false;
        //}
    //}
    public void Awake()
    {
        gms = GameObject.FindGameObjectWithTag("GMS").GetComponent<GMS>();
        
    }
    public override void OnNetworkSpawn()
    {
        
        
        

        //name = gms.PlayerTag;
        networkPlayerName.Value = "Player " + (OwnerClientId + 1);
        playerName.text = networkPlayerName.Value.ToString();

        

       // networkPlayerName.Value = "Player " + (OwnerClientId + 1);
        //playerName.text = networkPlayerName.Value.ToString();

        
    }
    

    public void Update()
    {
        //print(networkPlayerName.Value);
        //print(gms.PlayerTag);
       // name = networkPlayerName.Value.ToString();
        //networkPlayerName.Value = name;
        //playerName.text = name;
        //gms = GameObject.FindGameObjectWithTag("GMS").GetComponent<GMS>();
        //name = gms.PlayerTag;
        //playerName.text = gms.PlayerTag;

        
        //playerName.text = GameObject.Find("PlayerTag").ToString();

    }
}
