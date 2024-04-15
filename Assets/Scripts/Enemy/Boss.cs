using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float health;
    [SerializeField] private float checkRadius;
    [SerializeField] private float attackCD;
    private bool canAttack = true;
    [SerializeField] private float attackTime;
    public override float Health { get => health; set => health = value; }

    public override float CheckRadius => checkRadius;

    public override float AttackCD => attackCD;

    public override bool CanAttack => canAttack;

    public KnifeCircle knifesOrigin;

    public override void Attack()
    {
        int nextAttack = Random.Range(0, 2);
        if (nextAttack == 0)
        {
            StartCoroutine(KnifeRotate());
        }
    }

    public IEnumerator KnifeRotate()
    {
        canAttack = false;

        KnifeCircle knifes = Instantiate(knifesOrigin, this.transform.position, this.transform.rotation);

        yield return new WaitForSeconds(attackTime - 0.1f);

        Destroy(knifes);

        yield return new WaitForSeconds(0.1f);

        canAttack = true;
    }
}
