using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomMovement : MonoBehaviour 

{
    
    public float WalkSpeed;
    public float RunSpeed;
    
    public float range;
    
    public float maxDistance;
    Vector2 wayPoint;

    public GameObject target;
    public bool hasLineOfSight = false;
    public float distance;
    
    public GameObject detected;
    public Animator pAnimator;
    
    public float targetTime = 3f;

    public GameObject Vision;

    //public SlenderExample slender;
    


    void Start()
    {
       
        
        
        SetNewDestination();
    }

    
    void Update()
    {
        //target = slender.ForMovementTarget;
        print(distance);
        target = GameObject.FindGameObjectWithTag("Player");

        //pAnimator.SetFloat("distance", distance);

        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            SetNewDestination();
            targetTime = 3f;

        }
        

        float x = target.transform.position.x - transform.position.x;
        float y = target.transform.position.y - transform.position.y;

        

        distance = Vector3.Distance (transform.position, target.transform.position);
        

        

        if(hasLineOfSight && distance <= 3)
        {
            pAnimator.SetBool ("walk", false);
            pAnimator.SetBool ("run", false);
            pAnimator.SetBool ("idle", false);
            pAnimator.SetBool ("kill", true);

            //pAnimator.SetFloat("distance", 2);
            //pAnimator.Play("Z_Attack");



           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if (distance < 10)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = WalkSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            pAnimator.SetBool ("walk", false);
            pAnimator.SetBool ("run", true);
            pAnimator.SetBool ("idle", false);

            //pAnimator.SetFloat("distance", 8);
            //pAnimator.Play("Z_Run");


           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, RunSpeed * Time.deltaTime);
        }
        else if (distance <= 50)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = WalkSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            pAnimator.SetBool ("walk", true);
            pAnimator.SetBool ("run", false);
            pAnimator.SetBool ("idle", false);
            pAnimator.SetBool ("kill", false);

            //pAnimator.SetFloat(4f);
            //pAnimator.SetFloat("distance", 22);
            //pAnimator.Play("Z_Walk");

           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, WalkSpeed * Time.deltaTime);
        }
        

        


        

    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, target.transform.position - transform.position);
        if(ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Player");
            if(hasLineOfSight && distance < 80)
            {
                Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.green);
                detected.SetActive(true);
                
                
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WALL"))
        {
            pAnimator.SetBool("isMoving", true);
            pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));
            //wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
            wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y + Random.Range(-maxDistance, maxDistance));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            pAnimator.SetBool("isMoving", true);
            pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));
            //wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
            wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y + Random.Range(-maxDistance, maxDistance));
        }
    }

    void SetNewDestination()
    {
        

        
        
        

        wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y + Random.Range(-maxDistance, maxDistance));
        
        
    }

    
    
    
}