using System;
using UnityEngine;
using TMPro;

public class MessageWindow : MonoBehaviour
{
    public static MessageWindow instance;

    void Awake()
    {
        instance = this;
    }

    public TMP_Text message;
    public Action emptyHandler;

    private bool isChanged = false;
    private float existTime = 2f;
    private float existDelay;

    private void Update()
    {
        if (isChanged)
        {
            existDelay -= Time.deltaTime;
            if (existDelay <= 0)
            {
                message.text = "";
                isChanged = false;
                emptyHandler?.Invoke();
            }
        }
    }

    public new void SendMessage(string text)
    {
        message.text = text;
        isChanged = true;
        existDelay = existTime;
    }





}
