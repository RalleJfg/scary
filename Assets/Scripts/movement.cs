using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class movement : NetworkBehaviour
{
    public static movement instance;
    public float mouseSens = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public GMS gms;
    public Transform slender;
    public GameObject slenderCam;
    public Camera playercam;
    public GameObject player;

    
    float distance;
    //float nearestDistance = 10000;
    public float timer1 = 2;
    public float timer2 = 4;
    public GameObject deathScreen;
    public bool dead = false;


    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        slender = GameObject.Find("HeadTarget").transform;
        
        //deathScreen = GameObject.FindGameObjectWithTag("deathscreen");
        slenderCam = GameObject.FindGameObjectWithTag("slenderCamera");
        deathScreen.SetActive(false);
        slenderCam.SetActive(false);
    }

    void Update()
    {
        distance = Vector3.Distance(this.transform.position , slender.position);

        

        if(distance <= 3.5)
        {
            transform.LookAt(slender); //om man dör så kollar man på slender
            gms.CanMove = true;

            timer1 -= Time.deltaTime;
        }

        if(timer1 <= 0  || dead) // detta blir för båda spelarna. man delar tydligen på distance så det funkar inte att && det 
        {
            deathScreen.SetActive(true);
            timer2 -= Time.deltaTime;

        }
        //if(timer2 <= 0)
        //{
            //playercam.enabled = false;
            //slenderCam.SetActive(true);
        //    deathScreen.SetActive(false);
        //    player.SetActive(false);

        //    playercam.transform.parent = slender.transform;
        //    controller.enabled = false;
            
            
        //}
        

        

        gms = GameObject.FindGameObjectWithTag("GMS").GetComponent<GMS>();
        


        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;


        if (gms.CanMove == false && distance >= 3.5)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}
