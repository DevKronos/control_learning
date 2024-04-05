using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : Gun
{
    private float damage = 25f;
    private float impactForce = 0f;
    private int currentAmmo;

    public override float Damage => damage;

    public override float ImpactForce => impactForce;

    public override int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
}
