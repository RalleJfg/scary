using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerLeaderboard : MonoBehaviour
{
    
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    

    public GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length == 2)
        {
            p2.SetActive(true);
        }
        if(players.Length == 3)
        {
            p2.SetActive(true);
        }
        if(players.Length == 4)
        {
            p2.SetActive(true);
        }
    }
    
}
