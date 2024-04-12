using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : Item
{
    [SerializeField] private string color;
    [SerializeField] private Image itemSprite;


    public Image GetSprite()
    {
        return itemSprite;
    }

    public string GetColor()
    {
        return color;
    }

    public override void TakeAction()
    {
        PlayerManager.instance.FindKey(this);

        Destroy(this.gameObject);
    }
}
