using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lock : MonoBehaviour
{
    [SerializeField] private string color;
    public string Color { get => color; }
    /*
    public GameObject door;
    private bool isPressed = false;

    private void OnTriggerStay(Collider other)
    {
        PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
        if (player == null) return;
        if (!isPressed) MessageWindow.instance.SendMessage("Press F to open door");
        if (Input.GetKey(KeyCode.F))
        {
            if (PlayerManager.instance.HasKey(color))
            {
                PlayerManager.instance.LoseKey(color);
                Destroy(this.gameObject);
            }
            else
            {
                MessageWindow.instance.SendMessage($"You don't have {color} key :(");
                isPressed = true;
                MessageWindow.instance.emptyHandler = OpenSendMessage;
            }
        }
    }

    public void OpenSendMessage()
    {
        isPressed = false;
    }
    */
}
