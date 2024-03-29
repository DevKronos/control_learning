using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    private float health = 50f;
    private float checkRadius = 100f;
    private float damage = 5f;
    private float attackCD = 1f;
    private float reloadTime = 2f;
    private bool canAttack = true;

    public override float Health { get => health; set => health = value; }
    public override float CheckRadius => checkRadius;
    public override float AttackCD => attackCD;
    public override bool CanAttack => canAttack;

    public GameObject muzzleFlash;
    public Transform muzzle;
    public GameObject origin;
    public GameObject impactEffect;

    public override IEnumerator Attack()
    {
        GameObject muzzleGo = Instantiate(muzzleFlash,  muzzle.position, muzzle.rotation);
        Destroy(muzzleGo, 0.6f);

        RaycastHit hit;
        if (Physics.Raycast(origin.transform.position, origin.transform.forward, out hit, checkRadius))
        {

            IDamagable damagable = hit.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(damage);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);

            canAttack = false;

            yield return new WaitForSeconds(1f);

            animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(reloadTime - 0.25f);

            animator.SetBool("Reloading", false);

            yield return new WaitForSeconds(0.25f);

            canAttack = true;
        }
    }
}
