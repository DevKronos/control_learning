using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy
{
    private float health = 50f;
    private float checkRadius = 100f;
    private float attackCD = 4f;
    private bool canAttack = true;
    public Transform WeaponPos;

    public override float Health { get => health; set => health = value; }

    public override float CheckRadius => checkRadius;

    public override float AttackCD => attackCD;

    public override bool CanAttack => canAttack;

    public GameObject originBomb;
    public float throwForce = 1.05f;

    public override IEnumerator Attack()
    {
        canAttack = false;
        GameObject bomb = Instantiate(originBomb, WeaponPos.transform.position, WeaponPos.transform.rotation);
        Rigidbody bombBody = bomb.GetComponent<Rigidbody>();
        Vector3 dir = PlayerManager.instance.transform.position - this.transform.position;
        bombBody.AddForce(throwForce * dir, ForceMode.VelocityChange);

        canAttack = true;
        yield return null;
    }
}
