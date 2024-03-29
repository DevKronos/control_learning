using UnityEngine;
using TMPro;

public class AmountAmmo : MonoBehaviour
{
    public TMP_Text ammoText;
    private int maxAmmo;

    public void SetMaxAmmo(int amount)
    {
        maxAmmo = amount;
        SetAmount(maxAmmo);
    }

    public void SetAmount(int amount)
    {
        ammoText.text = $"{amount}/{maxAmmo}";
    }

    public void SetReload()
    {
        ammoText.text = "Reloading...";
    }
}
