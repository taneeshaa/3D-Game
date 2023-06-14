using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private Enemy enemy;
    private void Start()
    {

    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        



    }
    void Attack()
    {
        //Play attack animation
        anim.SetTrigger("attack");
    }
}