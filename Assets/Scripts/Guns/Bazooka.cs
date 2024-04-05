using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Gun
{
    private float damage = 50f;
    private float impactForce = 50f;
    private int currentAmmo;

    public override float Damage => damage;

    public override float ImpactForce => impactForce;

    public override int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }

    public Transform rocket;

    protected override void Shoot()
    {
        GameObject muzzleGo = Instantiate(muzzleFlash, muzzle.position, muzzle.rotation);
        Destroy(muzzleGo, 0.6f);

        CurrentAmmo--;
        StartCoroutine(Reload());

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            IDamagable damagable = hit.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(Damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1f);
        }
    }
}
