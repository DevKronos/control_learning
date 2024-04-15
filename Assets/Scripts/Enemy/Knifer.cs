using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knifer : Enemy
{
    private float health = 50f;
    private float checkRadius = 100f;
    private float damage = 5f;
    private float attackCD = 1f;
    private float attackTime = .2f;
    private bool canAttack = true;

    public override float Health { get => health; set => health = value; }
    public override float CheckRadius => checkRadius; 
    public override float AttackCD => attackCD; 
    public override bool CanAttack => canAttack;

    public override void Attack()
    {
        StartCoroutine(KnifeThrow());
    }

    public IEnumerator KnifeThrow()
    {
        canAttack = false;

        animator.SetBool("Attacking", true);

        yield return new WaitForSeconds(attackTime - 0.1f);

        if (InFieldAction())
        {
            PlayerManager.instance.TakeDamage(damage);
        }
        animator.SetBool("Attacking", false);

        yield return new WaitForSeconds(0.1f);

        canAttack = true;
    }


}
