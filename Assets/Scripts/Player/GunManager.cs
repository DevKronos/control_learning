using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunManager : MonoBehaviour
{
    public int selectedWeapon = 0;
    public List<Gun> guns;
    private void Update()
    {
        if (guns[selectedWeapon].IsReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            guns[selectedWeapon].TryReload();
        }
        if (Input.GetMouseButton(0))
        {
            guns[selectedWeapon].TryShoot();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            guns[selectedWeapon].SetAimMode();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= guns.Count - 1)
            {
                selectedWeapon = 0;
                SelectWeapon(selectedWeapon);
            }
            else
            {
                SelectWeapon(++selectedWeapon);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = guns.Count - 1;
                SelectWeapon(selectedWeapon);
            }
            else
            {
                SelectWeapon(--selectedWeapon);
            }
        }
    }
    private void SelectWeapon(int equip)
    {
        foreach (Gun gun in guns)
        {
            gun.gameObject.SetActive(false);
        }
        guns[equip].gameObject.SetActive(true);
    }

    public void AddWeapon(Gun origin)
    {
        origin.amountAmmo = PlayerManager.instance.playerAmmo;
        origin.fpsCam = PlayerManager.instance.playerCam;
        origin.gameObject.SetActive(false);
        Gun gun = Instantiate(origin, this.transform.position, this.transform.rotation, this.transform);
        guns.Add(gun);
    }
}
