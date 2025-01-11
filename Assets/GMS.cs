using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
using Unity.Services.Authentication;
using System;
using Unity.Services.Authentication;

using Unity.Services.Core;
using Unity.Services.Core.Environments;
using System.Threading.Tasks;



public class GMS : NetworkBehaviour
{
    
    public ReadyUp readyScript;
    public Animator animator;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public GameObject PauseMenu;
    public GameObject LobbyMenu;
    public bool CanMove = false;
    public TMP_InputField InputField;
    public string PlayerTag;
    public PlayerSettings pS;
    public GameObject Playernames;
    public GameObject lobby;
    



    // Start is called before the first frame update
    void start()
    {
        //Screen.SetResolution(400,224,true);

        //pS.user.Value = new NetString() { Uname = InputField.text };
        //pS.user.Value = new NetString() { Uname = InputField.text };
    }

    public void OnSubmit()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSettings>();
       // pS.user.Value = new NetString() { Uname = InputField.text };
        //pS.user.Value = new NetString("hej");
        
        if(lobby.activeInHierarchy == false)
        {
            Playernames.SetActive(true);
        }


        PlayerTag = InputField.text;

        if(readyScript.AllPlayersReady)
        {
            animator.SetBool("open", true);
            animator1.SetBool("open", true);
            animator2.SetBool("close", true);
            animator3.SetBool("close", true);
        }
        

        if (Input.GetKeyDown("p") && LobbyMenu.activeInHierarchy == false)
        {
            if(PauseMenu.activeInHierarchy == true)
            {
                PauseMenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                CanMove = false;
            }
            else
            {
                PauseMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CanMove = true;
            }

            
        }
    }
    
}
