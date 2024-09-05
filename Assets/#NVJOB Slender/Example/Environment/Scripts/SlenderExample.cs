// #NVJOB Nicholas Veselov - https://nvjob.github.io


using UnityEngine;
using UnityEngine.AI;

public class SlenderExample : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public SkinnedMeshRenderer body;
    public Transform target;
    public GameObject ForMovementTarget;
    public RandomMovement slenderMovement;
    public ReadyUp readyUp;
    public NavMeshAgent agent;
    public Animator pAnimator;
    public bool killed = false;
    private bool startTimer = false;


    public Transform head;
    public float blendTime = 0.4f;
    public float towards = 5.0f;
    public float weightMul = 1;
    public float clampWeight = 0.5f;
    public Vector3 weight = new Vector3(0.4f, 0.8f, 0.9f);
    public bool yTargetHeadSynk;
    public float timer1 = 2;
    public bool doneKilling = true;


    public GameObject[] players;
    float distance;
    float nearestDistance = 10000;

    //--------------

    Transform tr;
    Animator anim;
    AudioSource music;
    Vector3 lookAtTargetPosition, lookAtPosition;
    float lookAtWeight;
    float timeFaceCh, facepWeight = 100, timeFace = 10;
    bool faceCh = true;


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void Start()
    {
        //--------------

        tr = transform;
        anim = GetComponent<Animator>();
        music = GetComponent<AudioSource>();
        lookAtTargetPosition = target.position + tr.forward;
        lookAtPosition = head.position + tr.forward;

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    void Update()
    {
        print(distance);


        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i <players.Length; i++)
        {
            distance = Vector3.Distance(this.transform.position , players[i].transform.position);

            if(distance < nearestDistance)
            {
                ForMovementTarget = players[i];
                target = players[i].transform;
                nearestDistance = distance;
                head = players[i].transform;
            }
        }


        //--------------

        lookAtTargetPosition = target.position + tr.forward;

        //--------------

        if (faceCh == true && timeFace < Time.time)
        {
            timeFaceCh += Time.deltaTime * 80;
            if (timeFaceCh >= facepWeight * 2)
            {
                timeFaceCh = 0;
                faceCh = true;
                timeFace = Time.time + Random.Range(3.0f, 6.0f);
                music.pitch = Random.Range(0.8f, 1.0f);
            }
            float var0 = Mathf.PingPong(timeFaceCh, facepWeight);
            body.SetBlendShapeWeight(0, var0);
            music.volume = var0 * 0.1f;
        }

        //--------------


        if(readyUp.AllPlayersReady == true)
        {
            agent.SetDestination(ForMovementTarget.transform.position);
            //slenderMovement.enabled = true;       // slender movement startar inte förännn dörren öppnats
        }

        if(startTimer)
        {
            timer1 -= Time.deltaTime;
        }

        if(timer1 <= 0)
            {
                
                doneKilling = true;
                timer1 = 2;
                startTimer = false;
            }


        if(distance <= 3)
        {
            pAnimator.SetBool ("walk", false);
            pAnimator.SetBool ("run", false);
            pAnimator.SetBool ("idle", false);
            pAnimator.SetBool ("kill", true);

            doneKilling = false;
           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

            startTimer = true;
            
            agent.speed = 0;
            

            killed = true;

            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else if (distance < 8 && doneKilling)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            
            

            agent.speed = 4;

            pAnimator.SetBool ("walk", false);
            pAnimator.SetBool ("run", true);
            pAnimator.SetBool ("idle", false);
            pAnimator.SetBool ("kill", false);
           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

           
        }
        else if (distance <= 17 && doneKilling)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            
            

            agent.speed = 1;

            pAnimator.SetBool ("walk", true);
            pAnimator.SetBool ("run", false);
            pAnimator.SetBool ("idle", false);
            pAnimator.SetBool ("kill", false);
           // pAnimator.SetBool("isMoving", true);
            //pAnimator.SetFloat("moveX", (target.transform.position.x - transform.position.x));
            //pAnimator.SetFloat("moveY", (target.transform.position.y - transform.position.y));

            
        }
        else if (distance >= 17 && doneKilling)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            
            

            agent.speed = 0;

            pAnimator.SetBool ("walk", false);
            pAnimator.SetBool ("run", false);
            pAnimator.SetBool ("idle", true);
            pAnimator.SetBool ("kill", false);
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    void OnAnimatorIK()
    {
        //--------------

        if (yTargetHeadSynk == false) lookAtTargetPosition.y = head.position.y;
        Vector3 curDir = lookAtPosition - head.position;
        curDir = Vector3.RotateTowards(curDir, lookAtTargetPosition - head.position, towards * Time.deltaTime, float.PositiveInfinity);
        lookAtPosition = head.position + curDir;
        lookAtWeight = Mathf.MoveTowards(lookAtWeight, 1, Time.deltaTime / blendTime);
        anim.SetLookAtWeight(lookAtWeight * weightMul, weight.x, weight.y, weight.z, clampWeight);
        anim.SetLookAtPosition(lookAtPosition);

        //--------------
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}