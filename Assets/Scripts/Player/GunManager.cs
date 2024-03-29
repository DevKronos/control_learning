using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public int selectedWeapon = 0;
    public Gun[] guns;
    private void Update()
    {
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
            if (selectedWeapon >= guns.Length - 1)
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
                selectedWeapon = guns.Length - 1;
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
}
