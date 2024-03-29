using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    [SerializeField] private string color;
    [SerializeField] private Image itemSprite;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
        if (player == null) return;
        PlayerManager.instance.FindKey(this);

        Destroy(this.gameObject);
    }

    public Image GetSprite()
    {
        return itemSprite;
    }

    public string GetColor()
    {
        return color;
    }
}
