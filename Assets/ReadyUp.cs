using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyUp : MonoBehaviour
{
    public GMS gms;
    public int playersReady = 0;
    public bool AllPlayersReady = false;

    public GameObject[] players;
    public int playersInGame;

    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if(players.Length == 0)
        {
            playersInGame = -1;
        }
        else
        {
            playersInGame = players.Length;
        }


        if (playersReady == playersInGame)
        {
            
            AllPlayersReady = true;

            

            if (playersReady != playersInGame)
            {
                AllPlayersReady = true;


            
            }

            
        }
        //else
        //{
        //    AllPlayersReady = false;
        //}

        
    }
    
    public void Ready()
    {
        playersReady++;
    }
    public void notReady()
    {
        playersReady--;
    }

    

       
}
