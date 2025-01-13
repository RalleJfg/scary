using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 10;
    public Rigidbody rb;
    


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //print("yes");

        if (other.tag == "Sword")   //other.gameObject.tag == "Enemy"
        {


            
            
            health -= 3;
            rb.AddForce(20, 0, 0);

            if (health < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
