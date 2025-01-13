using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MouseLook : NetworkBehaviour
{
    public CharacterController controller;
    public float speed;
    public bool isReady = false;
    public Rigidbody rb;
    public Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    
    

    public ReadyUp readyUp;

    private float delay = 0;
    public GMS gms;

    
    public GameObject camera;
    
    
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        camera.SetActive(false);
       base.OnNetworkSpawn();
       if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
       camera.SetActive(true); // only enable YOUR PLAYER'S camera, all others will stay disabled
       
    }

    void Update()
    {
        if (IsServer)
        {
            //put server code here
        }


        readyUp = GameObject.FindGameObjectWithTag("ReadyUp").GetComponent<ReadyUp>();
        gms = GameObject.FindGameObjectWithTag("GMS").GetComponent<GMS>();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

        if (gms.CanMove == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(1.5f * -2f * -19.62f);
            }


            

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += -19.62f * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKey("left shift") && isGrounded)
            {
                speed = 5f;
            }
            else
            {
                speed = 4f;
            }
        }
        

        

        

        

        

        if(readyUp.AllPlayersReady)
        {
            
            delay += Time.deltaTime;

            if(delay < 2)
            {
                //transform.position = new Vector3(400, 0, 0);   //teleporterar alla till det riktiga rummet
            }
            

            

            //readyUp.playersReady = 0;
            //readyUp.AllPlayersReady = false;
            
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("ReadyUp"))
        {
            isReady = true;
            readyUp.Ready();
        }
    }
    public void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("ReadyUp"))
        {
            isReady = false;
            readyUp.notReady();
        }
    }

    //public override void OnNetworkSpawn()
    //{
    //    if(!IsOwner) Destroy(this);
    //}
}
