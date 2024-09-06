using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour

{
    public static WeaponController instance;

    public GameObject sword;
    public bool canAttack = true;
    public float AttackCooldown = 1.0f;
    
    public Animator animator;
    
    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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

    
}
