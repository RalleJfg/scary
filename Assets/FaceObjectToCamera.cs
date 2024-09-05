using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FaceObjectToCamera : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        
        
    }

    // Update is called once per frame

    
    void Update()
    {
        //if(IsOwner)
        //{
        //    transform.LookAt(Camera.main.transform);
            //target = GameObject.FindGameObjectWithTag("Player");
        //}
        //else if (!IsLocalPlayer)
        //{
        //    target = GameObject.FindGameObjectWithTag("Player");
        //}
        
        //transform.LookAt(target.transform);


        Camera cam = Camera.main;

        transform.LookAt(Camera.main.transform);

        //transform.LookAt(transform.position - cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
        
        
    }
}
