using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoot : Item
{
    [SerializeField] private Gun loot;
    [SerializeField] private GunManager weaponHolder;

    public override void TakeAction()
    {
        print("Bazooka here");
        weaponHolder.AddWeapon(loot);
        Destroy(this.gameObject);
    }
}
