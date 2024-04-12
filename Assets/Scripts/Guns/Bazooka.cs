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

    public Rocket rocket;
    public float speed;

    protected override void Shoot()
    {
        Rocket rocketGo = Instantiate(rocket, muzzle.position, muzzle.rotation);
        rocketGo.IsFired = true;
        rocketGo.ExplsEffect = impactEffect;

        Vector3 force = transform.forward;
        rocketGo.GetComponent<Rigidbody>().AddForce(speed * force);

        CurrentAmmo--;
        StartCoroutine(Reload());
    }

}
