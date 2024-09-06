using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour

{
    public GameObject sword;
    public bool canAttack = true;
    public float AttackCooldown = 1.0f;
    
    public Animator animator;
    public float health = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(canAttack )
            {
                SwordAttack();
                
            }
        }

        
    }

    public void SwordAttack()
    {
        
        canAttack = false;
        
        animator.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        
        canAttack = true;
    }

    public void OnCollisionEnter(Collision other)
    {
        //print("yes");

        if (other.gameObject.tag == "Enemy")
        {
            print("yes");
            health -= 3;
            if (health < 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
