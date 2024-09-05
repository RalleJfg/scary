using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerName : NetworkBehaviour
{
    public TextMeshProUGUI PlayerTag;
    //public TextMeshPro PlayerTag;
    public GMS gms;

    


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gms = GameObject.FindGameObjectWithTag("GMS").GetComponent<GMS>();

        PlayerTag.text = gms.PlayerTag;

        

        
    }
    
}
