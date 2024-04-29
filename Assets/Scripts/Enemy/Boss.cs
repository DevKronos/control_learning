using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private float health;
    [SerializeField] private float checkRadius;
    [SerializeField] private float attackCD;
    [SerializeField] private float attackTime;

    private bool canAttack = true;
    public override float Health { get => health; set => health = value; }

    public override float CheckRadius => checkRadius;

    public override float AttackCD => attackCD;

    public override bool CanAttack => canAttack;

    public KnifeCircle knifes;
    public GameObject LaserOrigin;
    public DeathRain rain;
    public Transform muzzle;

    public override void Attack()
    {
        canAttack = false;
        int nextAttack = Random.Range(0, 3);
        if (nextAttack == 0)
        {
            StartCoroutine(KnifeRotate());
        }
        else if (nextAttack == 1)
        {
            StartCoroutine(LaserShoot());
        }
        else if (nextAttack == 2)
        {
            StartCoroutine(DeathRain());
        }
    }

    public IEnumerator KnifeRotate()
    {
        knifes.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackTime);

        knifes.gameObject.SetActive(false);
        canAttack = true;
    }

    public IEnumerator LaserShoot()
    {
        GameObject explossion = Instantiate(LaserOrigin, muzzle.position, transform.rotation);
        Destroy(explossion, 1f);

        Collider[] colliders = Physics.OverlapCapsule(muzzle.GetChild(0).position, muzzle.GetChild(1).position, 5f);
        foreach (Collider strucked in colliders)
        {
            PlayerManager damagable = strucked.GetComponent<PlayerManager>();

            if (damagable != null) damagable.TakeDamage(15f);
        }
        yield return new WaitForSeconds(attackTime);

        canAttack = true;
    }

    public IEnumerator DeathRain()
    {
        rain.gameObject.SetActive(true);

        yield return new WaitForSeconds(attackTime);

        rain.gameObject.SetActive(false);
        rain.DestroyRain();
        canAttack = true;
    }
}
